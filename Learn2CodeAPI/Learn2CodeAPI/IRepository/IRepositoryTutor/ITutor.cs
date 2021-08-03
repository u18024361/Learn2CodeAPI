﻿using Learn2CodeAPI.Dtos.TutorDto;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.IRepository.IRepositoryTutor
{
    public interface ITutor
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<IEnumerable<Message>> GetSentMessages(string UserId);
        Task<Message> CreateMessage(MessageDto model);
        Task<IEnumerable<Message>> GetRecievedMessages(string UserId);
    }
}
