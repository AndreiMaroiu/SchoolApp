using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWPF.Models.Helpers
{
    public static class MeanHelper
    {
        private static SubjectMean GetGeneral(List<SubjectMean> means)
        {
            float general = 0f;

            foreach (var mean in means)
            {
                general += mean.Score;
            }

            return new SubjectMean(general / means.Count, "Media Generala");
        }

        private static float CalculateMean(List<SimpleNote> notes)
        {
            float sum = 0f;

            SimpleNote thesis = null;

            foreach (var note in notes)
            {
                sum += note.Score;

                if (note.IsThesis)
                {
                    thesis = note;
                }
            }

            float mean = 0f;

            if (thesis is not null)
            {
                mean = ((sum / notes.Count) * 3 + thesis.Score) / 4;
            }
            else
            {
                mean = sum / notes.Count;

                if (float.IsNaN(mean))
                {
                    mean = 0f;
                }
            }

            return mean;
        }

        public static List<SubjectMean> GetMeans(int studentID, int semesterID)
        {
            List<SubjectMean> means = new();

            Student student = DALHelper.GetObject<Student>("GetStudent", ("@id", studentID));

            List<Subject> subjects = DAL.GetSubjectsFromClass(student.Class_ID);

            foreach (var subject in subjects)
            {
                var notes = DAL.GetNotesSubject(studentID, semesterID ,subject.ID) as List<SimpleNote>;

                float mean = CalculateMean(notes);

                means.Add(new SubjectMean(mean, subject.SubjectName.Trim()));
            }

            means.Add(GetGeneral(means));

            return means;
        }

        public static SubjectMean GetGenerealMean(int studentID, int semesterID)
        {
            List<SubjectMean> means = new();

            Student student = DALHelper.GetObject<Student>("GetStudent", ("@id", studentID));

            List<Subject> subjects = DAL.GetSubjectsFromClass(student.Class_ID);

            foreach (var subject in subjects)
            {
                var notes = DAL.GetNotesSubject(studentID, semesterID ,subject.ID) as List<SimpleNote>;

                float mean = CalculateMean(notes);

                means.Add(new SubjectMean(mean, subject.SubjectName.Trim()));
            }

            return GetGeneral(means);
        }

        public static object Get(int studentId, int semesterID ,int? subjectID)
        {
            if (subjectID is null)
            {
                return GetMeans(studentId, semesterID);
            }

            float mean = CalculateMean(DAL.GetNotesSubject(studentId, semesterID ,subjectID) as List<SimpleNote>);
            List<float> result = new();
            result.Add(mean);
            return result;
        }
    }
}
