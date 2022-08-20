use goosleDB;
INSERT INTO goosleDB.University (on_quarantine ) VALUES (0);
INSERT INTO goosleDB.Departments (id_university, on_quarantine, dep_name) VALUES (1, 0, 'ИТКН');
INSERT INTO goosleDB.Departments (id_university, on_quarantine, dep_name) VALUES (1, 0, 'ЭКОТЕХ');
INSERT INTO goosleDB.Departments (id_university, on_quarantine, dep_name) VALUES (1, 0, 'ИНМИН');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role)
VALUES ('Дмитрий', 'Иванов', 'Геннадьевич', '1997-04-10', 24, '1224354643', '89705112343', 1, null, 's123456', '4354646', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role)
VALUES ('Олег', 'Сидиров', 'Евгеньевич', '1998-09-02', 23, '2343234654', '06112345902', 1, null, 's132454', '2342444', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role)
VALUES ('Татьяна', 'Альмухаметова', 'Руслановна', '1998-12-24', 23, '1243686744', '23464353635', 1, null, 's243523', '3535422', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role)
VALUES ('Инсур', 'Кунафин', 'Айдарович', '1999-03-29', 22, '2436467475', '14536464532', 1, null, 's453422', '3563411', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role)
VALUES ('Киркоров', 'Киркоров', 'Бедросович', '2000-10-11', 21, '4335463422', '85685433431', 1, null, 't124547', '1245554', 'Преподаватель');
INSERT INTO goosleDB.Teachers (id_person, Specializations) VALUES (5,'Старший преподаватель, к.т.н.');
INSERT INTO goosleDB.Group (id_teacher, name_group) VALUES (1,'МПИ-20-7-2');
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group)
VALUES (1,null,1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group)
VALUES (2,null,1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group)
VALUES (3,null,1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group)
VALUES (4,null,1);
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Инна', 'Соколова', 'Арнольдовна', '1980-04-21', 41, '2914593023', '89071112332', 1, null, 't123456', '1224356', 'Преподаватель');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Алина', 'Ксенофонтова', 'Андреевна', '1998-06-20', 23, '2909464837', '89034636272', 1, null, 's124433', '3454356', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Денис', 'Фролов', 'Тимофеевич', '1998-03-23', 23, '2914664837', '89034636270', 1, null, 's224433', '2354634', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Егор', 'Антипов', 'Максимович', '1970-09-22', 51, '2909461111', '89034636271', 2, null, 't120066', '2345432', 'Преподаватель');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Светлана', 'Кучерова', 'Николаевна', '1998-06-11', 23, '2909464830', '89034636274', 2, null, 's125554', '3566453', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Дарья', 'Кошель', 'Сергеевна', '1998-10-10', 23, '2909464835', '89034636273', 2, null, 's100433', '3787900', 'Студент');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Виктория', 'Копылова', 'Юрьевна', '1976-12-14', 45, '2909464836', '89034636275', 3, null, 't123421', '6649000', 'Преподаватель');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Эльдар', 'Рязанов', 'Тимурович', '1999-06-20', 22, '2909464831', '89034636276', 3, null, 's198763', '4457321', 'Студент');
INSERT INTO goosleDB.Teachers (id_person, Specializations) VALUES (11, 'Профессор математических наук');
INSERT INTO goosleDB.Teachers (id_person, Specializations) VALUES (17, 'Младший преподаватель');
INSERT INTO goosleDB.Departments (id_department, id_university, on_quarantine, dep_name) VALUES (666, 1, 0, 'ADMIN');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Маргарита', 'Суханкина', 'Андреевна', '1977-12-14', 44, '2909464837', '89034636277', 666, null, 'a100001', '3257763', 'Администрация');
INSERT INTO goosleDB.People (Name, Surname, Patronymic, Birthday, Age, Passport, TelephoneNumber, id_department, Photo, Entry_Card, Pass, Role) VALUES ('Сергей', 'Рублев', 'Дмитриевич', '1976-01-14', 45, '2909464838', '89034636278', 666, null, 'a100002', '3564711', 'Администрация');
INSERT INTO goosleDB.Offices (id_office, Lesson_office, Amount_of_places) VALUES (1, 'Б-601', 3);
INSERT INTO goosleDB.Offices (id_office, Lesson_office, Amount_of_places) VALUES (2, 'Б-602', 3);
INSERT INTO goosleDB.Offices (id_office, Lesson_office, Amount_of_places) VALUES (3, 'Б-907', 50);
INSERT INTO goosleDB.Offices (id_office, Lesson_office, Amount_of_places) VALUES (4, 'Б-904а', 25);
INSERT INTO goosleDB.Offices (id_office, Lesson_office, Amount_of_places) VALUES (5, 'К-311', 100);
INSERT INTO goosleDB.Administration (id_person, Post, id_office) VALUES (19, 'Проректор по безопасности', 1);
INSERT INTO goosleDB.Administration (id_person, Post, id_office) VALUES (20, 'Проректор по учебной части', 2);
INSERT INTO goosleDB.Teachers (id_person, Specializations) VALUES (14, 'Старший преподаватель');
INSERT INTO goosleDB.`Group` (id_teacher, name_group) VALUES (4, 'БМН-19-2');
INSERT INTO goosleDB.`Group` (id_teacher, name_group) VALUES (3, 'БЭН-19-1-4');
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (11, null, 1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (12, null, 1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (13, null, 1);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (15, null, 3);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (16, null, 3);
INSERT INTO goosleDB.Students (id_person, ScanCertificate_Of_PreviousEducation, id_group) VALUES (18, null, 4);
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Математический анализ 1 часть');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Математический анализ 2 часть');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Ряды и ТФКП');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Дифференциальные уравнения');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Уравнения в частных производных');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Физика 1 часть');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Физика 2 часть');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('История');
INSERT INTO goosleDB.Subjects (Sub_Name) VALUES ('Философия');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (1, '09:00:00', '10:35:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (2, '10:50:00', '12:25:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (3, '12:40:00', '14:15:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (4, '14:30:00', '16:05:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (5, '16:20:00', '17:55:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (6, '18:00:00', '19:25:00');
INSERT INTO goosleDB.Lesson_duration (id_lesson, Start, End) VALUES (7, '19:35:00', '20:55:00');
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 1, 1, 1, 'Понедельник', 1, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 1, 1, 2, 'Понедельник', 1, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 9, 3, 'Понедельник', 1, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 8, 2, 'Вторник', 1, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 3, 6, 3, 'Вторник', 1, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 3, 6, 4, 'Вторник', 1, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 9, 1, 'Четверг', 1, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 8, 3, 'Четверг', 1, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 1, 1, 4, 'Четверг', 1, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 1, 1, 1, 'Понедельник', 2, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 1, 1, 2, 'Понедельник', 2, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 9, 3, 'Понедельник', 2, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 8, 2, 'Вторник', 2, null, 4);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 3, 6, 3, 'Вторник', 2, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 9, 1, 'Четверг', 2, null, 3);
INSERT INTO goosleDB.Schedule (id_group, id_teacher, id_subject, id_lesson, day_of_week, WeekType, Link, id_office) VALUES (1, 4, 8, 3, 'Четверг', 2, null, 4);
INSERT INTO goosleDB.COVID (id_person, is_sick, ShouldCheck, Scan_Of_CoronaTestResults, DateStart, DateEnd) VALUES (3, 1, 1, null, '2021-04-01', null);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 1, 4, 1, 5);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 2, 4, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 3, 4, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 4, 4, 1, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 5, 4, 1, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 6, 4, 1, 4);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 1, 9, 7, 4, 1, 1);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 1, 4, 1, 5);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 2, 4, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 3, 4, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 4, 4, 1, 5);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 5, 4, 1, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 6, 4, 1, 4);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 3, 8, 7, 4, 1, 1);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 1, 1, 1, 5);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 2, 1, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 3, 1, 0, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 4, 1, 1, 3);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 5, 1, 1, 0);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 6, 1, 1, 4);
INSERT INTO goosleDB.Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) VALUES ('2021-04-05', 4, 1, 7, 1, 1, 1);


