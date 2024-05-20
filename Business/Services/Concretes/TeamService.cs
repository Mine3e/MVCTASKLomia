using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamService(ITeamRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddTeam(Team team)
        {
            if(team == null) throw new TeamNotFoundException("","Team null ola bilmez ");
            if (team.ImageFile == null) throw new ImageFileNotFoundException("ImageFile", "ImageFile null ola bilmez ");
            if (team.ImageFile.ContentType.Contains("img/")) throw new FileContentTypeException("ImageFile", "Content type error");
            if (team.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "ImageFile size error");
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Team\" + team.ImageFile.FileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                team.ImageFile.CopyTo(stream);
            }
            team.ImageUrl = team.ImageFile.FileName;
            _repository.Add(team);
            _repository.Commit();
        }

        public void DeleteTeam(int id)
        {
           var existteam=_repository.Get(x=> x.Id == id);
            if (existteam == null) throw new EntityNotFoundException("", "Entity not found ");
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Team\" + existteam.ImageUrl;
            if (!File.Exists(path)) throw new Business.Exceptions.FileNotFoundException("", "File not found ");
            File.Delete(path);
            _repository.Delete(existteam);
            _repository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
             return _repository.GetAll(func);
          
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
           return _repository.Get(func);
        }

        public void UpdateTeam (int id, Team team)
        {
            var existteam = _repository.Get(x => x.Id == id);
            if (existteam == null) throw new EntityNotFoundException("", "Entity not found ");
          
            if (team == null) throw new TeamNotFoundException("", "Team null ola bilmez ");
            
            if(team.ImageFile != null)
            {
                if (team.ImageFile.ContentType.Contains("img/")) throw new FileContentTypeException("ImageFile", "Content type error");
                if (team.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "ImageFile size error");
                string path1 = _webHostEnvironment.WebRootPath + @"\Upload\Team\" + team.ImageFile.FileName;
                using (FileStream stream = new FileStream(path1, FileMode.Create))
                {
                    team.ImageFile.CopyTo(stream);
                }
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Team\" + existteam.ImageUrl;
                if (!File.Exists(path)) throw new Business.Exceptions.FileNotFoundException("", "File not found ");
                File.Delete(path);
                existteam.ImageUrl = team.ImageFile.FileName;
            }
            existteam.Title = team.Title;
            existteam.Description = team.Description;
            existteam.Name = team.Name;
            _repository.Commit();
           
        }
    }
}
