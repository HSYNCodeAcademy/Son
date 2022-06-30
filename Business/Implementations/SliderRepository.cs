using Business.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class SliderRepository : ISlidersService
    {
        private readonly AppDbContext _context;

        public SliderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Slider> Get(int? id)
        {
            if(id is null)
            {
                throw new NotImplementedException();
            }

            var data = await _context.Sliders.Where(n=>!n.IsDeleted && n.Id==id).FirstOrDefaultAsync();

            if(data is null)
            {
                throw new NotImplementedException();
            }

            return data;
        }

        public async Task<List<Slider>> GetAll()
        {
            var  data = await _context.Sliders.Where(n=>!n.IsDeleted).ToListAsync();

            if (data is null)
            {
                throw new NotImplementedException();
            }
            return data;
        }

        public async Task Create(Slider entity)
        {
            if (entity is null)
            {
                throw new NotImplementedException();
            }
            entity.CreatedDate = DateTime.Now;
            await _context.Sliders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Slider entity)
        {
            if (entity is null)
            {
                throw new NotImplementedException();
            }

            var data = await Get(entity.Id);
            entity.UpdatedDate = DateTime.Now;
            _context.Sliders.Update(data);
            await _context.SaveChangesAsync(); 
        }

        public async Task Delete(int? id)
        {
            var data = await Get(id);
            data.IsDeleted = true;
            await Update(data);
            await _context.SaveChangesAsync();
        }
    }
}
