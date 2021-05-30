using DatabaseWPF.Models.DatabaseEntities;
using System;
using System.Collections.Generic;

namespace DatabaseWPF.Models.DataAccesLayer
{
    public class DAL
    {
        public static List<Person> GetPeople() => DALHelper.GetObjects<Person>("GetPersons");

        public static List<Student> GetStudents() => DALHelper.GetObjects<Student>("GetStudents");

        public static List<Professor> GetProfessors() => DALHelper.GetObjects<Professor>("GetProfessors");

        public static List<Specialization> GetSpecializations() => DALHelper.GetObjects<Specialization>("GetSpecializations");
        public static List<Subject> GetSubjects() => DALHelper.GetObjects<Subject>("GetSubjects");

        public static object GetNotesSubject(int studentID, int semesterID ,int? subjectID)
        {
            if (subjectID is null)
            {
                return DALHelper.GetObjects<NoteSubject>("GetNotesSubject", ("@studentID", studentID), ("@semesterID", semesterID));
            }

            return DALHelper.GetObjects<SimpleNote>("GetNotesFromSubject", ("@studentID", studentID), ("@subjectID", subjectID), ("@semesterID", semesterID));
        }

        public static object GetStudentAbsences(int studentID, int semesterID ,int? subjectID)
        {
            if(subjectID is null)
            {
                return DALHelper.GetObjects<AbsenceFull>("GetStudentAbseces", ("@id", studentID), ("@semesterID", semesterID));
            }

            return DALHelper.GetObjects<SimpleAbsence>("GetStudentAbsecesFromSubject", 
                ("@studentID", studentID), ("@subjectID", subjectID), ("@semesterID", semesterID));
        }

        public static object GetStudentAbsencesUnmotivated(int studentID, int semesterID, int? subjectID)
        {
            if (subjectID is null)
            {
                return DALHelper.GetObjects<AbsenceFull>("GetStudentAbsecesUnmotivated", ("@id", studentID), ("@semesterID", semesterID));
            }

            return DALHelper.GetObjects<SimpleAbsence>("GetStudentAbsecesFromSubjectUnmotivated", 
                ("@studentID", studentID), ("@subjectID", subjectID), ("@semesterID", semesterID));
        }

        public static List<Subject> GetSubjectsFromClass(int classID)
        {
            return DALHelper.GetObjects<Subject>("GetSubjectsFromClass", ("@classID", classID));
        }

        public static List<StudentFull> GetStudentsFull()
        {
            return DALHelper.GetObjects<StudentFull>("GetStudentsFull");
        }

        public static List<ProfessorFull> GetProfessorsFull()
        {
            return DALHelper.GetObjects<ProfessorFull>("GetProfessorsFull");
        }

        public static List<SimpleNote> GetStudentNotesFromSubject(int studentID, int subjectID)
        {
            return DALHelper.GetObjects<SimpleNote>("GetStudentNotesFromSubject",
                                                    ("@studentID", studentID), ("@subjectID", subjectID));
        }

        public static void AddPerson(string firstName, string lastName)
        {
            DALHelper.ExecuteNonQuery("AddPerson", ("@firstName", firstName), ("@lastName", lastName));
        }

        public static void AddUser(string username, string password, string role, int personID)
        {
            DALHelper.ExecuteNonQuery("AddUser", ("@username", username), ("@password", password),
                                                ("@role", role), ("@personID", personID));
        }

        public static void AddStudent(string firstName, string lastName, int year, string yearChar)
        {
            DALHelper.ExecuteNonQuery("AddStudent", ("@firstName", firstName), ("@lastName", lastName),
                                                        ("@year", year), ("@yearChar", yearChar));
        }

        public static void AddNewStudent(int personID, int classID)
        {
            DALHelper.ExecuteNonQuery("AddNewStudent", ("@personID", personID), ("@classID", classID));
        }

        public static void AddSimpleProfessor(int personID, int subjectID)
        {
            DALHelper.ExecuteNonQuery("AddSimpleProfessor", ("@personID", personID), ("@subjectID", subjectID));
        }

        public static void UpdateAbsence(int id, DateTime date, bool motivated)
        {
            DALHelper.ExecuteNonQuery("UpdateAbsence", ("@id", id), ("@date", date), ("@motivated", motivated));
        }

        public static void AddMasterProfessor(int personID, int subjectID, int classID)
        {
            DALHelper.ExecuteNonQuery("AddMasterProfessor", ("@personID", personID), ("@subjectID", subjectID)
                                                            , ("@classID", classID));
        }

        public static void AddClass(int year, string yearChar)
        {
            DALHelper.ExecuteNonQuery("AddClass", ("@year", year), ("@yearChar", yearChar));
        }

        public static void AddSubject(string name)
        {
            DALHelper.ExecuteNonQuery("AddSubject", ("@name", name));
        }

        public static void AddSpecilization(string name)
        {
            DALHelper.ExecuteNonQuery("AddSpecialization", ("@name", name));
        }

        public static void AddClassSpecialization(int classID, int specializationID)
        {
            DALHelper.ExecuteNonQuery("AddClassSpec", ("@classID", classID), ("@specializationID", specializationID));
        }

        public static void AddClassSubject(int classID, int subjectID)
        {
            DALHelper.ExecuteNonQuery("AddClassSubject", ("@classID", classID), ("@subjectID", subjectID));
        }

        public static void AddSubjectSpec(int subjectID, int specializationID, bool haveThesis)
        {
            DALHelper.ExecuteNonQuery("AddSubjectSpecialization", ("@subjectID", subjectID), ("@specializationID", specializationID),
                                                                    ("@haveThesis", haveThesis));
        }

        public static void AddProfessorClass(int professorID, int classID)
        {
            DALHelper.ExecuteNonQuery("AddProfessorClass", ("@professorID", professorID), ("@classID", classID));
        }

        public static List<Class> GetClasses()
        {
            return DALHelper.GetObjects<Class>("GetClasses");
        }

        public static ClassSubject GetClassSubject(int classID, int subjectID)
        {
            return DALHelper.GetObject<ClassSubject>("GetClassSubject", ("@classID", classID), ("@subjectID", subjectID));
        }

        public static List<Class> GetClassesFromProfessor(int professorID)
        {
            return DALHelper.GetObjects<Class>("GetClassesFromProfessor", ("@professorID", professorID));
        }

        public static void AddNote(int score, int subjectID, int studentID, DateTime date, bool isThesis, int semesterID)
        {
            DALHelper.ExecuteNonQuery("AddNote", ("@score", score), ("@subjectID", subjectID),
                    ("@studentID", studentID), ("@date", date), ("@isThesis", isThesis), ("@semesterID", semesterID));
        }

        public static void UpdateNote(int id, int score, DateTime date, bool isThesis)
        {
            DALHelper.ExecuteNonQuery("UpdateNote", ("@id", id), ("@score", score), ("@date", date), ("@isThesis", isThesis));
        }

        public static void AddAbsence(DateTime date, int studentID, int subjectID, int semesterID)
        {
            DALHelper.ExecuteNonQuery("AddAbsence", ("@date", date), ("@studentID", studentID),
                        ("@subjectID", subjectID), ("@motivated", false), ("@semesterID", semesterID));
        }

        public static void AddSemester(int number, int year)
        {
            DALHelper.ExecuteNonQuery("AddSemester", ("@number", number), ("@year", year));
        }

        public static void AddCourse(string name, byte[] data, string extention, object description, int classSubjectID)
        {
            DALHelper.ExecuteNonQuery("AddCourse", ("@name", name), ("@data", data), ("@extension", extention),
                                        ("@description", description), ("@classSubjectID", classSubjectID));
        }
    }
}
