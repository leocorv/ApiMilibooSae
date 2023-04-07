using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class ProduitManager : IDataRepositoryProduits<Produit>
    {
        readonly MilibooDBContext? milibooDBContext;

        readonly VarianteManager? varianteManager;

        readonly CategorieManager? categorieManager;

        readonly DeleteAllCycles? deleteAllCycles;

        private int nbrArticleParPage = 5;

        public ProduitManager() { }

        public ProduitManager(MilibooDBContext context)
        {
            milibooDBContext = context;
            varianteManager = new VarianteManager(context);
            categorieManager = new CategorieManager(context);
            deleteAllCycles = new DeleteAllCycles(context);
        }

        public async Task AddAsync(Produit entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit entity)
        {
            milibooDBContext.Produits.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAll()
        {
            var cat = await milibooDBContext.Categories.ToListAsync<Categorie>();
            foreach (var categorie in cat)
            {
                categorie.SousCategoriesNavigation = null;
            }
            var variante = await milibooDBContext.Variantes.ToListAsync<Variante>();

            var produits = await milibooDBContext.Produits.ToListAsync<Produit>();
            foreach (Produit pd in produits)
            {
                foreach (var lavar in pd.VariantesProduitNavigation)
                {
                    lavar.ProduitVarianteNavigation = null;
                }
                pd.CategorieProduitNavigation.ProduitsCategorieNavigation = null;
            }
            return produits;
        }

        public async Task<ActionResult<Produit>> GetProduitById(int produitId)
        {
            var leProduit = await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(p => p.IdProduit == produitId);
            var categorie = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(c => c.Categorieid == leProduit.CategorieId);
            categorie.ProduitsCategorieNavigation = null;
            var variantes = await milibooDBContext.Variantes.Where<Variante>(var => var.IdProduit == produitId).ToListAsync();
            var couleurs = new List<Couleur>();
            var avis = new List<Avis>();
            var photos = new List<Photo>();
            for (int i = 0; i < variantes.Count; i++)
            {
                couleurs.Add(await milibooDBContext.Couleurs.FirstOrDefaultAsync<Couleur>(c => c.IdCouleur == variantes[i].IdCouleur));
                avis.Add(await milibooDBContext.Avis.FirstOrDefaultAsync<Avis>(a => a.VarianteId == variantes[i].IdVariante));
                photos.Add(await milibooDBContext.Photos.FirstOrDefaultAsync<Photo>(photo => photo.VarianteId == variantes[i].IdVariante));
                variantes[i].ProduitVarianteNavigation = null;

            }

            foreach (Couleur couleur in couleurs) { couleur.VariantesCouleurNavigation = null; }

            foreach (Avis avi in avis)
            {
                if (avi != null)
                {
                    avi.VarianteAvisNavigation = null;
                }
            }

            foreach (Photo photo in photos)
            {
                if (photo != null)
                {
                    photo.VariantePhotoNavigation = null;
                }
            }

            return leProduit;
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCategorie(int categorieId)
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            if(category == null)
            {
                return new List<Produit>();
            }
            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            List<Produit> allProducts = new List<Produit>();

            foreach (Categorie cat in allCategoriesChilds)
            {
                List<Produit> rawData = milibooDBContext.Produits.Where<Produit>(p => p.CategorieId == cat.Categorieid).ToList();

                foreach (Produit prd in rawData)
                {
                    allProducts.Add(prd);
                }
            }

            return allProducts.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorie(int page, int categorieId)
        {
            var resultProduitVar = await GetAllByCategorie(categorieId);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCollection(int collectionId)
        {
            return await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCollection(int page, int collectionId)
        {

            var rawDataVar = await GetAllByCollection(collectionId);
            List<Produit> rawData = rawDataVar.Value.ToList();
            return DecouperListe(page, rawData);
        }



        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleur(int page, int categorieId, List<int> couleurId )
        {
            var resultProduitVar = await GetAllByCouleur(categorieId, couleurId);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsIdByCouleur(List<int> couleurId)
        {
            var lesVariantes = await milibooDBContext.Variantes.Where<Variante>(var => couleurId.Contains(var.IdCouleur)).ToListAsync();
            var lesProduits = await milibooDBContext.Produits.Where(p => lesVariantes.Select(v => v.IdVariante).Contains(p.IdProduit)).ToListAsync();

            return lesProduits;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCouleur(int categorieId, List<int> couleurId)
        {
            var resultProduitCategorieVar = await GetAllByCategorie(categorieId);
            List<Produit> ResultProduitCategorie = resultProduitCategorieVar.Value.ToList();

            var resultProduiCouleurVar = await GetProduitsIdByCouleur(couleurId);
            List<Produit> ResultProduitCouleur = resultProduiCouleurVar.Value.ToList();

            List<Produit> finalList = ResultProduitCategorie.Intersect(ResultProduitCouleur).ToList();

            return finalList.ToList();
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPrixMini(int categorieId, double minprix)
        {
            var resultProduitCategorieVar = await GetAllByCategorie(categorieId);
            List<Produit> ResultProduitCategorie = resultProduitCategorieVar.Value.ToList();

            var rawData = await milibooDBContext.Produits.Where(p => p.VariantesProduitNavigation.Any(v => v.Prix >= minprix)).ToListAsync();

            List<Produit> finalList = ResultProduitCategorie.Intersect(rawData).ToList();

            return finalList.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMini(int page, int categorieId, double minprix)
        {
            var resultProduitVar = await GetAllByPrixMini(categorieId, minprix);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }



        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPrixMaxi(int categorieId, double maxprix)
        {
            var resultProduitCategorieVar = await GetAllByCategorie(categorieId);
            List<Produit> ResultProduitCategorie = resultProduitCategorieVar.Value.ToList();

            var rawData = await milibooDBContext.Produits.Where(p => p.VariantesProduitNavigation.Any(v => v.Prix <= maxprix)).ToListAsync();

            List<Produit> finalList = ResultProduitCategorie.Intersect(rawData).ToList();

            return finalList.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMaxi(int page, int categorieId, double maxprix)
        {
            var resultProduitVar = await GetAllByPrixMaxi(categorieId, maxprix);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }

        public async Task<ActionResult<Produit>> GetByStringAsync(string libelle)
        {
            return await milibooDBContext.Produits.FirstOrDefaultAsync(u => u.Libelle.ToUpper() == libelle.ToUpper());
        }


        public async Task<decimal> GetNumberPagesByCategorie(int categorieId)
        {
            var nbrArticlesVar = await GetAllByCategorie(categorieId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }

        public async Task<decimal> GetNumberPagesByCollection(int collectionId)
        {
            var nbrArticlesVar = await GetAllByCollection(collectionId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }

        public async Task<decimal> GetNumberPagesByCouleur(int categorieId, List<int> couleurId)
        {
            var nbrArticlesVar = await GetAllByCouleur(categorieId, couleurId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }

        public async Task<decimal> GetNumberPagesByPrixMini(int categorieId, double minprix)
        {
            var nbrArticlesVar = await GetAllByPrixMini(categorieId, minprix);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }

        public async Task<decimal> GetNumberPagesByPrixMaxi(int categorieId, double maxprix)
        {
            var nbrArticlesVar = await GetAllByPrixMaxi(categorieId, maxprix);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.CategorieId = entity.CategorieId;
            entityToUpdate.CollectionId = entity.CollectionId;
            entityToUpdate.Description= entity.Description;
            entityToUpdate.DensiteAssise = entity.DensiteAssise;
            entityToUpdate.DensiteDossier = entity.DensiteDossier;
            entityToUpdate.DimAccoudoir = entity.DimAccoudoir;
            entityToUpdate.DimAssise = entity.DimAssise;
            entityToUpdate.DimColis = entity.DimColis;
            entityToUpdate.DimDeplie = entity.DimDeplie;
            entityToUpdate.DimDossier = entity.DimDossier;
            entityToUpdate.DimTotale = entity.DimTotale;
            entityToUpdate.HauteurPieds = entity.HauteurPieds;
            entityToUpdate.IdProduit = entity.IdProduit;
            entityToUpdate.InscructionsEntretien = entity.InscructionsEntretien;
            entityToUpdate.Libelle = entity.Libelle;
            entityToUpdate.Matiere = entity.Matiere;
            entityToUpdate.MatierePieds = entity.MatierePieds;
            entityToUpdate.PoidsColis = entity.PoidsColis;
            entityToUpdate.Revetement = entity.Revetement;
            entityToUpdate.TypeMousseAssise = entity.TypeMousseAssise;
            entityToUpdate.TypeMousseDossier = entity.TypeMousseDossier;

            await milibooDBContext.SaveChangesAsync();
        }

        public ActionResult<IEnumerable<Produit>> DecouperListe(int page, List<Produit> rawData)
        {
            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                for (int i = (page - 1) * nbrArticleParPage; i < rawData.Count; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public List<int> ConvertCategoriesIntoIds(List<Categorie> categories) 
        {
            List<int> allCategoriesInt = new List<int>();
            foreach (Categorie c in categories)
            {
                allCategoriesInt.Add(c.Categorieid);
            }

            return allCategoriesInt;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllFilters(int? categorieId, int? collectionId, List<int>? couleurId, double? maxprix, double? minprix)
        {
            var cat = await milibooDBContext.Categories.ToListAsync<Categorie>();
            foreach (var categorie in cat)
            {
                categorie.SousCategoriesNavigation = null;
            }
            var variante = await milibooDBContext.Variantes.ToListAsync<Variante>();
            var couleurs = await milibooDBContext.Couleurs.ToListAsync<Couleur>();
            foreach (Couleur couleur in couleurs)
            {
                couleur.VariantesCouleurNavigation = null;
            }
            var avis = await milibooDBContext.Avis.ToListAsync<Avis>();
            foreach (Avis avi in avis)
            {
                avi.VarianteAvisNavigation = null;
            }
            var photos = await milibooDBContext.Photos.ToListAsync<Photo>();
            foreach (Photo photo in photos)
            {
                photo.VariantePhotoNavigation = null;
            }

            var productsAfterFilterCat = await GetAll();
            var productsAfterFilterCollection = await GetAll();
            var productsAfterFilterColors = await GetAll();
            var productsAfterFilterMaxPrice = await GetAll();
            var productsAfterFilterMinPrice = await GetAll();

            if (categorieId != null)
            {
                productsAfterFilterCat = await GetAllByCategorie((int)categorieId);
                if (couleurId.Count() != 0)
                {
                    productsAfterFilterColors = await GetAllByCouleur((int)categorieId, couleurId);
                }
                if (maxprix != null)
                {
                    productsAfterFilterMaxPrice = await GetAllByPrixMaxi((int)categorieId, (int)maxprix);
                }
                if (minprix != null)
                {
                    productsAfterFilterMinPrice = await GetAllByPrixMini((int)categorieId, (int)minprix);
                }
            }
            if (collectionId != null)
            {
                productsAfterFilterCollection = await GetAllByCollection((int)collectionId);
            }

            List<Produit> productsAfterFilterCatList = productsAfterFilterCat.Value.ToList();
            List<Produit> productsAfterFilterCollectionList = productsAfterFilterCollection.Value.ToList();
            List<Produit> productsAfterFilterColorsList = productsAfterFilterColors.Value.ToList();
            List<Produit> productsAfterFilterMaxPriceList = productsAfterFilterMaxPrice.Value.ToList();
            List<Produit> productsAfterFilterMinPriceList = productsAfterFilterMinPrice.Value.ToList();

            List<Produit> finalList = productsAfterFilterCatList.Intersect(productsAfterFilterCollectionList).ToList();
            finalList = finalList.Intersect(productsAfterFilterColorsList).ToList();
            finalList = finalList.Intersect(productsAfterFilterMaxPriceList).ToList();
            finalList = finalList.Intersect(productsAfterFilterMinPriceList).ToList();

            //if(valeurTri == 1)
            //{
            //    var listri = finalList.OrderBy(p => p.Libelle);
            //}

            return (List<Produit>)finalList;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllFiltersByPage(int page, int? categorieId, int? collectionId, List<int>? couleurId, double? maxprix, double? minprix)
        {
            var resultProduitVar = await GetByAllFilters(categorieId, collectionId, couleurId, maxprix, minprix);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }

        //public async Task<ActionResult<IEnumerable<Produit>>> GetByAllFiltersByPage(int page, int? categorieId, int? collectionId, List<int>? couleurId, double? maxprix, double? minprix, int valeurTri)
        //{
        //    var resultProduitVar = await GetByAllFilters(categorieId, collectionId, couleurId, maxprix, minprix, valeurTri);
        //    List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

        //    return DecouperListe(page, resultProduit.ToList());
        //}

        public async Task<decimal> GetNumberPagesByAllFilters(int? categorieId, int? collectionId, List<int>? couleurId, double? maxprix, double? minprix)
        {
            var nbrArticlesVar = await GetByAllFilters(categorieId, collectionId, couleurId, maxprix, minprix);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            Console.WriteLine("COUNT : " + nbrArticles.Count());
            Console.WriteLine("ARTICLES : " + nbrArticleParPage);
            Console.WriteLine("CALCUL : " + ((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage));
            Console.WriteLine("PAGES : " + Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage));

            return Math.Ceiling((decimal)nbrArticles.Count() / (decimal)nbrArticleParPage);
        }
    }
}
