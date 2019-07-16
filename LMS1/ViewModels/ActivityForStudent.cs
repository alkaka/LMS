﻿using LMS1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS1.ViewModels
{
    public class ActivityForStudent
    {
        public CourseActivity activity { get; set; }
        //We have limited the number to 1, so a list is not needed
        public List<ExerciseSubmission> submissions { get; set; }
    }
}
