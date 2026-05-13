using System.Threading.Tasks;
using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet_store.Controllers;

public class UrunController : Controller
{
    private readonly DataContext _context;
    public UrunController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index(int? kategori)
    {

        var query = _context.Urunler.AsQueryable();

        if (kategori != null)
        {
            query = query.Where(i => i.KategoriId == kategori);
        }

        var urunler = query.Select(i => new UrunGetModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Fiyat = i.Fiyat,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriAdi = i.Kategori.KategoriAdi,
            Resim = i.Resim
        }).ToList();
        ViewBag.Kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi", kategori);

        return View(urunler);
    }

    // http://localhost:5162/urunler/telefon?q=apple
    // http://localhost:5162/urunler?q=apple
    // route params: url => value
    // query string: q   => value
    public ActionResult List(string url, string q)
    {
        // Sadece aktif urunleri getiren temel sorgu olusturulur.
        var query = _context.Urunler.Where(i => i.Aktif); //Queryable

        if (!string.IsNullOrEmpty(url))
        {
            // Url bilgisi geldiyse urunler kategoriye gore filtrelenir.
            query = query.Where(i => i.Kategori.Url == url);
        }

        if (!string.IsNullOrEmpty(q))
        {
            // Arama metni geldiyse urun adinda gecen kayitlar bulunur.
            // Buyuk-kucuk harf farkini kaldirmak icin iki taraf da kucuk harfe cevrilir.
            query = query.Where(i => i.UrunAdi.ToLower().Contains(q.ToLower()));

            ViewData["q"] = q;
        }

        // Sorgu calistirilir ve sonuc liste olarak View'e gonderilir.
        // var urunler = _context.Urunler.Where(i => i.Aktif && i.Kategori.Url == url).ToList();
        return View(query.ToList());
    }

    public ActionResult Details(int id)
    {
        var urun = _context.Urunler.Find(id);

        if (urun == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["BenzerUrunler"] = _context.Urunler
                                        .Where(i => i.Aktif && i.KategoriId == urun.KategoriId && i.Id != id)
                                        .Take(4)
                                        .ToList();

        return View(urun);
    }

    [HttpGet]
    public ActionResult Create()
    {
        // ViewBag.Kategoriler = _context.Kategoriler.ToList();
        ViewBag.Kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UrunCreateModel model)
    {
        if (model.Resim == null || model.Resim.Length == 0)
        {
            ModelState.AddModelError("Resim", "Resim girmelisiniz.");
        }

        if (ModelState.IsValid)
        {
            var fileName = Path.GetRandomFileName() + ".jpg";  //dosya adi
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName); //resmin kaydedilme yeri

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Resim!.CopyToAsync(stream);
            }


            var entity = new Urun
            {
                UrunAdi = model.UrunAdi,
                Aciklama = model.Aciklama,
                Fiyat = model.Fiyat ?? 0,
                Aktif = model.Aktif,
                Anasayfa = model.Anasayfa,
                KategoriId = (int)model.KategoriId!,
                Resim = fileName
            };
            _context.Urunler.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View(model);
    }

    public ActionResult Edit(int id)
    {
        var entity = _context.Urunler.Select(i => new UrunEditModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Fiyat = i.Fiyat,
            ResimAdi = i.Resim,
            Aciklama = i.Aciklama,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriId = i.KategoriId
        }).FirstOrDefault(i => i.Id == id);
        ViewBag.Kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View(entity);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, UrunEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var entity = _context.Urunler.FirstOrDefault(i => i.Id == id);

            if (entity != null)
            {

                if (model.Resim != null)
                {
                    var fileName = Path.GetRandomFileName() + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.Resim!.CopyToAsync(stream);
                    }

                    entity.Resim = fileName;
                }
                entity.Id = model.Id;
                entity.UrunAdi = model.UrunAdi;
                entity.Fiyat = model.Fiyat ?? 0;
                // entity.Resim = model.ResimAdi;
                entity.Aciklama = model.Aciklama;
                entity.Aktif = model.Aktif;
                entity.Anasayfa = model.Anasayfa;
                entity.KategoriId = (int)model.KategoriId!;


                _context.SaveChanges();

                TempData["Mesaj"] = $"{entity.UrunAdi} urunu guncellendi.";

                return RedirectToAction("Index");
            }
        }
        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id != null)
        {
            RedirectToAction("Index");
        }

        var entity = _context.Urunler.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            return View(entity);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id != null)
        {
            RedirectToAction("Index");
        }

        var entity = _context.Urunler.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Urunler.Remove(entity);
            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.UrunAdi} urun silindi.";

        }
        return RedirectToAction("Index");
    }


}
