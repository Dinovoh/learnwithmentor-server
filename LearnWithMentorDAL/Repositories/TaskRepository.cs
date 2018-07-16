﻿using System.Collections.Generic;
using System.Linq;
using LearnWithMentorDAL.Entities;
using LearnWithMentorDAL.Repositories.Interfaces;

namespace LearnWithMentorDAL.Repositories
{
    public class TaskRepository: BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(LearnWithMentor_DBEntities context) : base(context)
        {
        }

        public Task Get(int id)
        {
            return Context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public bool IsRemovable(int id)
        {
            return (!Context.PlanTasks.Any(pt=>pt.Task_Id==id));
        }

        public IEnumerable<Task> Search(string[] str, int planId)
        {
            if (!Context.Plans.Any(p => p.Id == planId))
                return null;
            List<Task> result = new List<Task>();
            foreach (var s in str)
            {
                IQueryable<Task> found = Context.PlanTasks.Where(p => p.Plan_Id == planId)
                                             .Select(t => t.Tasks)
                                             .Where(t => t.Name.Contains(s));
                foreach (var f in found)
                {
                    if (!result.Contains(f))
                    {
                        result.Add(f);
                    }
                }
            }
            return result;
        }

        public IEnumerable<Task> Search(string[] str)
        {
            List<Task> result = new List<Task>();
            foreach (var s in str)
            {
                IQueryable<Task> found = Context.Tasks.Where(t => t.Name.Contains(s));
                foreach (var f in found)
                {
                    if (!result.Contains(f))
                    {
                        result.Add(f);
                    }
                }
            }
            return result;
        }
    }
}
