﻿using LearnWithMentorDAL.Entities;
using System.Collections.Generic;

namespace LearnWithMentorDAL.Repositories
{
    public interface IGroupRepository: IRepository<Group>
    {
        Group Get(int id);
        IEnumerable<Group> GetGroupsByMentor(int mentor_id);
    }
}
