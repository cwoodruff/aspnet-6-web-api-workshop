﻿using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories
{
    public interface IPlaylistRepository : IDisposable
    {
        Task<List<Playlist>> GetAll();
        Task<Playlist> GetById(int id);
        Task<Playlist> Add(Playlist newPlaylist);
        Task<bool> Update(Playlist playlist);
        Task<bool> Delete(int id);
        Task<List<Playlist>> GetByTrackId(int id);
    }
}