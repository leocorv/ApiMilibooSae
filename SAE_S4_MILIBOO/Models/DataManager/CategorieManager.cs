using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Collections.Generic;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CategorieManager : IDataRepositoryCategorie<Categorie>
    {
        readonly MilibooDBContext? milibooDBContext;

        public CategorieManager() { }

        public CategorieManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }


        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid== id);
        }

        public async Task<ActionResult<Categorie>> GetParent(int id)
        {
            Categorie? leParent = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.CategorieParentid == id);
            if(leParent != null)
                leParent.SousCategoriesNavigation = null;
            return leParent;

        }

        public async Task<ActionResult<List<Categorie>>> GetCategoriesPremierNiveau()
        {
            var categories = await milibooDBContext.Categories.ToListAsync<Categorie>();
            if (categories != null)
            {
                foreach (Categorie cat in categories) { cat.CategorieParentNavigation = null; }
            }

            List<Categorie> result = new List<Categorie>();
            foreach(Categorie cat in categories)
            {
                if(cat.CategorieParentid == null)
                {
                    result.Add(cat);
                }
            }

            return result;
        }

        public async Task<ActionResult<List<Categorie>>> GetSousCategories(int id)
        {
            var lesCategories =  await milibooDBContext.Categories.Where<Categorie>(cat => cat.CategorieParentid == id).ToListAsync<Categorie>();

            if(lesCategories != null)
                foreach(Categorie cat in lesCategories) { cat.CategorieParentNavigation = null; } 
            return lesCategories;
        }

        public async Task<ActionResult<List<Categorie>>> RecursivelyAllChildsCategories(Categorie cat)
        {
            List<Categorie> allCategoriesChilds = new List<Categorie>();

            var sousCatVar = await GetSousCategories(cat.Categorieid);
            List<Categorie> sousCat = sousCatVar.Value;

            Console.WriteLine("nbr : " + sousCat.Count()); ;
            if (sousCat.Count() != 0)
            {
                Console.WriteLine("nbr : " + sousCat.Count()); ;
                foreach (Categorie laSousCat in sousCat)
                {
                    var subCategories = await RecursivelyAllChildsCategories(laSousCat);
                    allCategoriesChilds.AddRange(subCategories.Value);
                }

                foreach (Categorie c in allCategoriesChilds)
                {
                    Console.WriteLine(c.Categorieid);
                }
            }

            allCategoriesChilds.Add(cat);

            return allCategoriesChilds;
        }
    }
}
