using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InfinityDataModel
{
    public class InfinityDataAccess
    {
        private readonly InfinityEntities dbContext;
        public InfinityDataAccess(InfinityEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Misc> GetMisc()
        {
            using (this.dbContext)
            {
                return this.dbContext.Misc
                    .Select(o => o)
                    .ToList();
            }
        }

        public async Task<IEnumerable<Misc>> GetMiscAsync()
        {
            using (this.dbContext)
            {
                return await this.dbContext.Misc
                    .Select(o => o)
                    .ToListAsync();
            }
        }

        public bool InsertMisc(string data, string description)
        {
            try
            {
                using (this.dbContext)
                {
                    var newModel = new Misc
                    {
                        Data = data,
                        Description = description
                    };

                    this.dbContext.Misc.Add(newModel);
                    this.dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertMiscAsync(string data, string description)
        {
            try
            {
                using (this.dbContext)
                {
                    var newModel = new Misc
                    {
                        Data = data,
                        Description = description
                    };

                    this.dbContext.Misc.Add(newModel);
                    await this.dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateMisc(int miscId, string data, string description)
        {
            try
            {
                using (this.dbContext)
                {
                    var dataModel = this.dbContext.Misc.Find(miscId);

                    if (dataModel != null)
                    {
                        dataModel.Data = data;
                        dataModel.Description = description;
                        this.dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMiscAsync(int miscId, string data, string description)
        {
            try
            {
                using (this.dbContext)
                {
                    var dataModel = await this.dbContext.Misc.FindAsync(miscId);

                    if (dataModel != null)
                    {
                        dataModel.Data = data;
                        dataModel.Description = description;
                        await this.dbContext.SaveChangesAsync();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteMisc(int miscId)
        {
            try
            {
                using (this.dbContext)
                {
                    var dataModel = this.dbContext.Misc.Find(miscId);

                    if (dataModel != null)
                    {
                        this.dbContext.Misc.Remove(dataModel);
                        this.dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMiscAsync(int miscId)
        {
            try
            {
                using (this.dbContext)
                {
                    var dataModel = await this.dbContext.Misc.FindAsync(miscId);

                    if (dataModel != null)
                    {
                        this.dbContext.Misc.Remove(dataModel);
                        await this.dbContext.SaveChangesAsync();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
