﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderServiceChallenge.Data;
using OrderServiceChallenge.Models;
using OrderServiceChallenge.Services.Exceptions;

namespace OrderServiceChallenge.Services
{
    public class OrderServiceService
    {
        private readonly OrderServiceChallengeContext _context;

        public OrderServiceService(OrderServiceChallengeContext context)
        {
            _context = context;
        }

        public async Task<List<OrderService>> FindAllAsync()
        {
            return await _context.OrderService.ToListAsync();
        }

        public async Task InsertAsync(OrderService obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderService> FindByIdAsync(int id)
        {
            return await _context. OrderService.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.OrderService.FindAsync(id);
            _context.OrderService.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderService obj)
        {
            bool hasAny = await _context.OrderService.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);

            }

        }

        public bool OrderServiceExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

    }
}
