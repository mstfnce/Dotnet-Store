using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class KategoriController : Controller
{

    private readonly DataContext _context;

    public KategoriController(DataContext context)
    {
        _context = context;
    }

    // Displays all categories with their related product counts.
    public ActionResult Index()
    {
        var kategoriler = _context.Kategoriler.Select(i => new KategoriGetModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url,
            UrunSayisi = i.Uruns.Count
        }).ToList();

        return View(kategoriler);
    }

    // Shows the create form.
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    // Creates a new category from the submitted form values.
    [HttpPost]
    public ActionResult Create(KategoriCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = new Kategori
            {
                KategoriAdi = model.KategoriAdi,
                Url = model.Url
            };

            _context.Kategoriler.Add(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(model);
    }


    // Loads the selected category into the edit form.
    [HttpGet]
    public ActionResult Edit(int id)
    {
        var entity = _context.Kategoriler.Select(i => new KategoriEditModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }

    // Handles the edit form submission and saves the updated category data.
    [HttpPost]
    public ActionResult Edit(int id, KategoriEditModel model)
    {
        // The id coming from the URL must match the id posted from the form.
        // If they are different, the request is considered invalid and the user is sent back to the list.
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            // Finds the existing category record in the database by its id.
            // We update the existing entity instead of creating a new one.
            var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == id);

            if (entity != null)
            {
                // Copies the edited values from the view model into the database entity.
                entity.KategoriAdi = model.KategoriAdi;
                entity.Url = model.Url;

                // Saves the modified values to the database.
                _context.SaveChanges();

                TempData["Mesaj"] = $"{entity.KategoriAdi} kategorisi guncellendi.";

                // After a successful update, returns to the category list page.
                return RedirectToAction("Index");
            }
        }

        // If no matching category is found, the same form is returned with the posted values.
        return View(model);
    }


    public ActionResult Delete(int? id)
    {
        if (id != null)
        {
            RedirectToAction("Index");
        }

        var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == id);

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

        var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Kategoriler.Remove(entity);
            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.KategoriAdi} kategorisi silindi.";

        }
        return RedirectToAction("Index");
    }
}
