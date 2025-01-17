﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderServiceChallenge.Data;
using OrderServiceChallenge.Models;
using OrderServiceChallenge.Services;

namespace OrderServiceChallenge.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }
        
        // GET: Index
        public async Task<IActionResult> Index()
        {
            var list = await _companyService.FindAllAsync();
            return View(list);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyService.FindByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CNPJ,CEP,Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                await _companyService.InsertAsync(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyService.FindByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CNPJ,CEP,Id,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _companyService.UpdateAsync(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_companyService.CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyService.FindByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _companyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
