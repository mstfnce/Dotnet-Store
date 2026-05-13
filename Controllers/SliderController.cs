using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class SliderController : Controller
{

    private readonly DataContext _context;
    public SliderController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        return View(_context.Sliderlar.Select(i => new SliderGetModel
        {
            Id = i.Id,
            Aktif = i.Aktif,
            Baslik = i.Baslik,
            Resim = i.Resim,
            Index = i.Index
        }).ToList());
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(SliderCreateModel model)
    {
        if (model.Resim == null || model.Resim.Length == 0)
        {
            ModelState.AddModelError("Resim", "Resim girmelisiniz.");
        }

        if (ModelState.IsValid)
        {
            var fileName = Path.GetRandomFileName() + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Resim!.CopyToAsync(stream);
            }

            var entity = new Slider
            {
                Baslik = model.Baslik,
                Aciklama = model.Aciklama,
                Resim = fileName,
                Index = model.Index ?? 0,
                Aktif = model.Aktif
            };

            _context.Sliderlar.Add(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var entity = _context.Sliderlar.Select(i => new SliderEditModel
        {
            Id = i.Id,
            Baslik = i.Baslik ?? string.Empty,
            Aciklama = i.Aciklama,
            ResimAdi = i.Resim,
            Index = i.Index,
            Aktif = i.Aktif
        }).FirstOrDefault(i => i.Id == id);

        if (entity == null)
        {
            return RedirectToAction("Index");
        }

        return View(entity);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, SliderEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var entity = _context.Sliderlar.FirstOrDefault(i => i.Id == id);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            if (model.Resim != null)
            {
                var fileName = Path.GetRandomFileName() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Resim.CopyToAsync(stream);
                }

                entity.Resim = fileName;
            }

            entity.Baslik = model.Baslik;
            entity.Aciklama = model.Aciklama;
            entity.Index = model.Index ?? 0;
            entity.Aktif = model.Aktif;

            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.Baslik} isimli slider guncellendi.";

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Sliderlar.FirstOrDefault(i => i.Id == id);

        if (entity == null)
        {
            return RedirectToAction("Index");
        }

        return View(entity);
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Sliderlar.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Sliderlar.Remove(entity);
            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.Baslik} isimli slider silindi.";
        }

        return RedirectToAction("Index");
    }
}
