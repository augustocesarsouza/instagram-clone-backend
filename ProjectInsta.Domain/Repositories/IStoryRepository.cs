﻿using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IStoryRepository
    {
        public Task<ICollection<Story>> GetAllStory();
        public Task<Story> GetByIdAsync(int id);
        public Task<Story> GetIdStoryToUpdate(int idStory);
        public Task<List<Story>> GetByUserIdAsync(int userCreatePost);
        public Task<Story> CreateAsync(Story story);
        public Task<Story> UpdateAsync(Story story);
        public Task<Story> DeleteAsync(Story story);
    }
}
