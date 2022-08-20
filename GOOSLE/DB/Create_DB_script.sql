use goosleDB;
CREATE TABLE goosleDB.University
(
    id_university int AUTO_INCREMENT PRIMARY KEY,
    on_quarantine bool
);
CREATE TABLE goosleDB.Departments
(
    id_department int AUTO_INCREMENT PRIMARY KEY,
    id_university int,
    dep_name varchar(10),
    on_quarantine bool,
    FOREIGN KEY (id_university) REFERENCES goosleDB.University (id_university) ON DELETE CASCADE
);
CREATE TABLE goosleDB.People
(
    id_person       int AUTO_INCREMENT PRIMARY KEY,
    Name            tinytext,
    Surname         tinytext,
    Patronymic      tinytext,
    Birthday        date,
    Age             int,
    Passport        varchar(20),
    TelephoneNumber text,
    id_department   int,
    Photo           blob,
    Entry_Card      varchar(7),
    Pass            tinytext,
    Role            tinytext,
    FOREIGN KEY (id_department) REFERENCES goosleDB.University (id_university) ON DELETE SET NULL
);
CREATE TABLE goosleDB.COVID
(
    id_person                 int,
    is_sick                   bool,
    ShouldCheck               bool,
    Scan_Of_CoronaTestResults blob,
    DateStart                 date,
    DateEnd                   date,
    FOREIGN KEY (id_person) REFERENCES goosleDB.People (id_person) ON DELETE CASCADE
);
CREATE TABLE goosleDB.Teachers
(
    id_teacher      int AUTO_INCREMENT PRIMARY KEY,
    id_person       int,
    Specializations text,
    FOREIGN KEY (id_person) REFERENCES goosleDB.People (id_person) ON DELETE CASCADE
);
CREATE TABLE goosleDB.Offices
(
    id_office        int PRIMARY KEY,
    Lesson_office    varchar(9),
    Amount_of_places int
);
CREATE TABLE goosleDB.Administration
(
    id_administration int AUTO_INCREMENT PRIMARY KEY,
    id_person  int,
    Post       varchar(55),
    id_office  int,
    FOREIGN KEY (id_person) REFERENCES goosleDB.People (id_person) ON DELETE CASCADE,
    FOREIGN KEY (id_office) REFERENCES goosleDB.Offices (id_office) ON DELETE SET NULL

);
CREATE TABLE goosleDB.Group
(
    id_group   int AUTO_INCREMENT PRIMARY KEY,
    id_teacher int,
    name_group varchar(12),
    FOREIGN KEY (id_teacher) REFERENCES goosleDB.Teachers (id_teacher) ON DELETE SET NULL
);
CREATE TABLE goosleDB.Students
(
    id_student                           int AUTO_INCREMENT PRIMARY KEY,
    id_person                            int,
    ScanCertificate_Of_PreviousEducation blob,
    id_group                             int,
    FOREIGN KEY (id_person) REFERENCES goosleDB.People (id_person) ON DELETE CASCADE,
    FOREIGN KEY (id_group) REFERENCES goosleDB.Group (id_group) ON DELETE SET NULL
);
CREATE TABLE goosleDB.Subjects
(
    id_subject int AUTO_INCREMENT PRIMARY KEY,
    Sub_Name   text,
    Curriculum text
);
CREATE TABLE goosleDB.Lesson_duration
(
    id_lesson int PRIMARY KEY,
    Start     time,
    End       time
);
CREATE TABLE goosleDB.Attendance
(
    id_date    date,
    id_lesson  int,
    id_subject int,
    id_student int,
    id_teacher int,
    Check_     boolean,
    points     int,
    FOREIGN KEY (id_subject) REFERENCES goosleDB.Subjects (id_subject) ON DELETE SET NULL,
    FOREIGN KEY (id_student) REFERENCES goosleDB.Students (id_student) ON DELETE CASCADE,
    FOREIGN KEY (id_lesson) REFERENCES goosleDB.Lesson_duration (id_lesson) ON DELETE SET NULL,
    FOREIGN KEY (id_teacher) REFERENCES goosleDB.Teachers (id_teacher) ON DELETE SET NULL

);
CREATE TABLE goosleDB.Schedule
(
    id_schedule int AUTO_INCREMENT PRIMARY KEY,
    id_group    int,
    id_teacher  int,
    id_subject  int,
    id_lesson   int,
    day_of_week varchar(12),
    WeekType    int,
    Link        text,
    id_office   int,
    FOREIGN KEY (id_group) REFERENCES goosleDB.Group (id_group) ON DELETE NO ACTION,
    FOREIGN KEY (id_lesson) REFERENCES goosleDB.Lesson_duration (id_lesson) ON DELETE NO ACTION,
    FOREIGN KEY (id_subject) REFERENCES goosleDB.Subjects (id_subject) ON DELETE NO ACTION,
    FOREIGN KEY (id_teacher) REFERENCES goosleDB.Teachers (id_teacher) ON DELETE NO ACTION,
    FOREIGN KEY (id_office) REFERENCES goosleDB.Offices (id_office) ON DELETE NO ACTION
);