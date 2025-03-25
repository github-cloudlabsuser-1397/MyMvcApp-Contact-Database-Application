using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Devuelve la lista de usuarios a la vista
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Devuelve la vista para crear un nuevo usuario
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Agrega el nuevo usuario a la lista
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
            // Busca el usuario existente por ID
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }

            // Actualiza los datos del usuario
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
        }

        // Elimina el usuario de la lista
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
