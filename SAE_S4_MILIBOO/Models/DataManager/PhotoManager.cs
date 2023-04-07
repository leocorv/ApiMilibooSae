using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class PhotoManager : IDataRepositoryPhoto<Photo>
    {
        readonly MilibooDBContext? milibooDBContext;

        public PhotoManager() { }

        public PhotoManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task<ActionResult<Photo>> GetPhotoById(int photoId)
        {
            return await milibooDBContext.Photos.FirstOrDefaultAsync<Photo>(p => p.PhotoId == photoId);
        }

        public async Task<ActionResult<IEnumerable<Photo>>> GetAllPhotosByAvis(int avisId)
        {
            return await milibooDBContext.Photos.Where<Photo>(p => p.AviId == avisId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<string>>> GetAllPhotosByVariante(int varianteId)
        {
            var lesPhotos =  await milibooDBContext.Photos.Where<Photo>(p => p.VarianteId == varianteId).Select(p => p.Chemin).ToListAsync();

            return lesPhotos;
        }

        public async Task AddAsync(Photo entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Photo entity)
        {
            milibooDBContext.Photos.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Photo entityToUpdate, Photo entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.PhotoId = entity.PhotoId;
            entityToUpdate.AviId = entity.AviId;
            entityToUpdate.VarianteId = entity.VarianteId;
            entityToUpdate.CategorieId = entity.CategorieId;
            entityToUpdate.Chemin = entity.Chemin;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
