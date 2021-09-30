using AspGestContact.Mappers;
using AspGestContact.Models;
using AspGestContact.Models.Forms;
using AspGestContact.Sessions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspGestContact.Controllers
{
    public class ContactController : Controller
    {
        private IContactClientRepo _contactService;

        public ContactController(IContactClientRepo contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Contact Page";
            ViewData["Year"] = DateTime.Now.Year;
            return View(_contactService.Get(SessionHelper.User.Id, SessionHelper.User.Token).Select(c => c.ClientToAsp()));
        }

        public IActionResult Details(int id)
        {
            ContactAsp contact = _contactService.Get(SessionHelper.User.Id, id).ClientToAsp();
            if(contact is null)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddContactForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            ContactAsp contact = new ContactAsp()
            {
                LastName = form.LastName,
                FirstName = form.FirstName,
                Email = form.Email,
                Phone = form.Phone,
                BirthDate = form.BirthDate,
                UserId = SessionHelper.User.Id
            };
            _contactService.Insert(contact.AspToClient(), SessionHelper.User.Token);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ContactAsp contact = _contactService.Get(SessionHelper.User.Id, id).ClientToAsp();
            if(contact is null)
            {
                return RedirectToAction("Index");
            }
            EditContactForm form = new EditContactForm()
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                Email = contact.Email,
                Phone = contact.Phone,
                BirthDate = contact.BirthDate,
            };
            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditContactForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            ContactAsp contact = new ContactAsp()
            {
                LastName = form.LastName,
                FirstName = form.FirstName,
                Email = form.Email,
                Phone = form.Phone,
                BirthDate = form.BirthDate,
                UserId = SessionHelper.User.Id
            };
            _contactService.Update(id, contact.AspToClient(), SessionHelper.User.Token);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            ContactAsp contact = _contactService.Get(SessionHelper.User.Id, id).ClientToAsp();
            if (contact is null)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection form)
        {
            _contactService.Delete(SessionHelper.User.Id, id, SessionHelper.User.Token);
            return RedirectToAction("Index");
        }

    }
}
