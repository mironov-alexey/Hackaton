﻿using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CharacterController : Controller
    {
        private ICharacterRepository repository;
        public CharacterController(ICharacterRepository productRepository)
        {
            this.repository = productRepository;
        }

        public List<Character> FilterByField(string fieldName, string fieldValue)
        {
            return repository.Characters.ToList()
                .Where(x => x.GetType().GetProperty(fieldName).GetValue(x).ToString() == fieldValue)
                .ToList();

        }
        public List<Character> FilterByParameters(string name, bool isAlive, Sex sex)
        {
            return repository.Characters
                .Where(c => c.Name.Contains(name) && c.IsAlive == isAlive && c.Sex == sex).ToList();
        }

        [HttpPost]
        public ViewResult List(string name, bool isAlive, Sex sex)
        {
            var view = View(new MainModel(FilterByParameters(name, isAlive, sex), Request.RawUrl));
            view.ViewBag.Name = name;
            view.ViewBag.IsAlive = isAlive;
            view.ViewBag.Sex = sex;
            return view;
        }

        public ViewResult List(string fieldName, string fieldValue)
        {
            if (fieldValue == null && fieldName == null)
                return View(new MainModel(repository.Characters, Request.RawUrl));
            return View(new MainModel(FilterByField(fieldName, fieldValue), Request.RawUrl));
        }
    }
}
