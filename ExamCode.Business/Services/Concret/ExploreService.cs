using ExamCode.Business.Exceptions;
using ExamCode.Business.Services.Abstract;
using ExamCode.Core.Models;
using ExamCode.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExamCode.Business.Services.Concret;

public class ExploreService : IExploreService
{
    private readonly IExploreRepository _exploreRepository;
    private readonly IWebHostEnvironment _env;

    public ExploreService(IExploreRepository exploreRepository, IWebHostEnvironment env)
    {
        _exploreRepository = exploreRepository;
        _env = env;
    }
    public async Task AddAsyncExplore(Explore explore)
    {
        if (explore.ImageFile == null) throw new FileNullException("File bos ola bilmez!");

        if(explore.ImageFile.ContentType != "image/png" && explore.ImageFile.ContentType != "image/jpeg")
        {
            throw new FileContentException("File uygun deyil!");
        }
        if(explore.ImageFile.Length> 2097152)
        {
            throw new FileSizeException("File olcusu 2mb artiq ola bilmez!");
        }

        string fileName= Guid.NewGuid().ToString() + Path.GetExtension(explore.ImageFile.FileName);

        string path= _env.WebRootPath + "\\uploads\\explores\\" + fileName;

        using(FileStream fileStream= new FileStream(path, FileMode.Create))
        {
            explore.ImageFile.CopyTo(fileStream);
        }

        explore.ImageUrl= fileName;

         await _exploreRepository.AddAsync(explore);
         await _exploreRepository.CommitAsync();
    }

    public void DeleteExplore(int id)
    {
        var exsist= _exploreRepository.Get(x=>x.Id==id);

        if (exsist == null) throw new IdNullException("Id bos ola bilmez!");

        string path = _env.WebRootPath + "\\uploads\\explores\\" + exsist.ImageUrl;

        if(!File.Exists(path))
        {
            throw new FileNotExsistException("file tapilmadi!");
        }

        File.Delete(path);

        _exploreRepository.Delete(exsist);
        _exploreRepository.Commit();

    }

    public List<Explore> GetAllExplores(Func<Explore, bool>? func = null)
    {
        return _exploreRepository.GetAll(func);
    }

    public Explore GetExplore(Func<Explore, bool>? func = null)
    {
        return _exploreRepository.Get(func);
    }

    public void UpdateExplore(int id, Explore newExplore)
    {
        var oldExplore = _exploreRepository.Get(x => x.Id == id);

        if (oldExplore == null) throw new IdNullException("Id bos ola bilmez!");

        if(newExplore.ImageFile != null)
        {
            if (newExplore.ImageFile.ContentType != "image/png" && newExplore.ImageFile.ContentType != "image/jpeg")
            {
                throw new FileContentException("File uygun deyil!");
            }
            if (newExplore.ImageFile.Length > 2097152)
            {
                throw new FileSizeException("File olcusu 2mb artiq ola bilmez!");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newExplore.ImageFile.FileName);

            string path = _env.WebRootPath + "\\uploads\\explores\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                newExplore.ImageFile.CopyTo(fileStream);
            }

            string oldPath = _env.WebRootPath + "\\uploads\\explores\\" + oldExplore.ImageUrl;

            if (!File.Exists(oldPath))
            {
                throw new FileNotExsistException("file tapilmadi!");
            }

            File.Delete(oldPath);

            oldExplore.ImageUrl= fileName;
        }

        oldExplore.Title= newExplore.Title;
        oldExplore.Content= newExplore.Content;
        oldExplore.Description= newExplore.Description;

        _exploreRepository.Commit();
    }
}
