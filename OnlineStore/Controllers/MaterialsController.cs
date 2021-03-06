﻿using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineStore.DataLayer;
using OnlineStore.Models;


namespace OnlineStore.Controllers
{
    public class MaterialsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();
        /// <summary>
        /// تعریف کالا
        /// </summary>
        /// <param name="material">کالا</param>
        /// <returns></returns>
        // POST: api/Materials
        [ResponseType(typeof(Material))]
        public IHttpActionResult DefinitionMaterial(Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //validation
                #region Uniqe Material Code
                bool isExistCode = material.IsUniqueMaterialCode(material.MaterialCode);
                if (isExistCode)
                {
                    return BadRequest(Messages.MaterialCodeMustBeUniqe);
                }
                #endregion

                #region Uniqe Material Title In Group
                bool isExistTitleInGroup = material.IsUniqueMaterialTitleInGroup(material.MaterialTitle, material.MaterialGroupId);
                if (isExistTitleInGroup)
                {
                    return BadRequest(Messages.MaterialTitleInGroupMustBeUniqe);

                }
                #endregion

                db.Materials.Add(material);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = material.MaterialId }, material);
            }
            catch (System.Exception)
            {
                return BadRequest(ModelState);
            }
           
           
        }

       
        // GET: api/Materials
        public IQueryable<Material> GetMaterials()
        {
            return db.Materials;
        }

      
    }
}