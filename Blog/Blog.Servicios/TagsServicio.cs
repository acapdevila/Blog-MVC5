﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

namespace Blog.Servicios
{
    public class TagsServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;

        public TagsServicio(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        public IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));
        }

        public List<Tag> BuscarTags(CriteriosBusqueda criteriosBusqueda)
        {
            if (criteriosBusqueda == CriteriosBusqueda.Vacio())
                return new List<Tag>();

            return Tags()
                .Where(m => criteriosBusqueda.PalabrasBuscadas.Contains(m.Nombre))
                .ToList();

        }

        public async Task<List<Tag>> ObtenerListaTagsViewModel()
        {
            return await Tags().OrderBy(m=>m.Nombre).ToListAsync();
        }

        public List<Tag> TagsConPostsPublicados()
        {
            return Tags().ConPostsPublicados().OrderBy(m=>m.Nombre).ToList();
        }

        public async Task<Tag> RecuperarTag(int id)
        {
            return await Tags().FirstAsync(m => m.Id == id);
        }

        public async Task CrearTag(EtiquetaDto etiquetaDto)
        {
            var etiqueta = new Tag();
            etiqueta.CopiarValores(etiquetaDto);
            _db.Tags.Add(etiqueta);
            await _db.SaveChangesAsync();
            etiquetaDto.Id = etiqueta.Id;
         }

        public async Task ActualizarTag(EtiquetaDto etiquetaDto)
        {
            var tag = await RecuperarTag(etiquetaDto.Id);
            tag.CopiarValores(etiquetaDto);
            await _db.SaveChangesAsync();
         }

        public async Task EliminarTag(int id)
        {
            var tag = await RecuperarTag(id);
            _db.Tags.Remove(tag);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        
    }
}
