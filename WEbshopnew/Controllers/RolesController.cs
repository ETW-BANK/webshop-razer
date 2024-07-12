using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using webshoping.Models;

namespace webshoping.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole role)

        {
            if (ModelState.IsValid) 
            { 

            var result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {

                   
                ModelState.AddModelError("", "Role name already exists.");
                return View(role);
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
                else
                {
                 
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(role);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }


            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(string id, IdentityRole Updatedrole)
        {
            if (id != Updatedrole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _roleManager.FindByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }

                result.Name = Updatedrole.Name;

                var role = await _roleManager.UpdateAsync(result);
                if (role.Succeeded)
                {
                    return RedirectToAction("Index");
                }

              
            }

            return View(Updatedrole);
        }
    }



    
}