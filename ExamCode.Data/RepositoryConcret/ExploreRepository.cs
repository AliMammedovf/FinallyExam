using ExamCode.Core.Models;
using ExamCode.Core.RepositoryAbstract;
using ExamCode.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCode.Data.RepositoryConcret;

public class ExploreRepository : GenericRepository<Explore>, IExploreRepository
{
    public ExploreRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
