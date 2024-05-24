using ExamCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCode.Business.Services.Abstract;

public interface IExploreService
{
    Task AddAsyncExplore(Explore explore);

    void DeleteExplore(int id); 

    void UpdateExplore(int id, Explore newExplore);

    Explore GetExplore(Func<Explore, bool>? func=null);

    List<Explore> GetAllExplores(Func<Explore, bool>? func = null);
}
