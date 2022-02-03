import models.model
import models.model as model

def getProcessedHospitalisations():
    hospitalisations = model.HospitacjaModel.getAll()
    processedHospitalisations = []

    for hospitalisation in hospitalisations:
        hospDict = hospitalisation.toDict()
        teacher = model.ProwadzacyModel.getById(hospDict["prowadzacy_id"])
        if teacher is not None:
            hospDict["tytul_imie_nazwisko"] = str(teacher)
        else:
            hospDict["tytul_imie_nazwisko"] = "None"
        processedHospitalisations.append(hospDict)
    return processedHospitalisations


def getDictTeacherCourses():
    groups = models.model.GrupaZajeciowaModel.getAll()
    teachersIds = []
    courseCodes = []

    for group in groups:
        teachersIds.append(group.prowadzacy_id)
        courseCodes.append(group.kurs_kod)

    setOfIds = set(teachersIds)
    teacherCourses = {}
    for teacherId in setOfIds:
        teacherCourses[teacherId] = set()

    for i in range(len(teachersIds)):
        teacherCourses[teachersIds[i]].add(courseCodes[i])

    return teacherCourses

def getTeachersWithCourse():
    teachers = []
    for teacherId in getDictTeacherCourses().keys():
        teachers.append(models.model.ProwadzacyModel.getById(teacherId))
    return teachers

def isDone(hospitalisation_id):
    protocols = model.ProtokolModel.getAll()
    for protocol in protocols:
        if protocol.hospitacja_id == hospitalisation_id:
            return protocol.data_wystawienia is not None
    else:
        return False


def getHospTeamByHospId(hospitalisation_id: int):
    hospitalisation = model.HospitacjaModel.getById(hospitalisation_id)
    if hospitalisation is None:
        return None
    team = model.ZespolHospitujacyModel.getById(hospitalisation.zespol_hospitujacy_id)
    return team

def teamMembers(team_id):
    assignments = model.ProwadzacyZespolHospitujacyModel.getAll()
    members = []
    for assignment in assignments:
        if assignment.zespol_id == team_id:
            members.append(models.model.ProwadzacyModel.getById(assignment.prowadzacy_id))
    return members


def getTeacherTeamsIds(teacher_id:int):
    teacher = models.model.ProwadzacyModel.getById(teacher_id)
    if teacher is None:
        return []
    assignments = models.model.ProwadzacyZespolHospitujacyModel.getAll()

    teachersTeams = []
    for assignment in assignments:
        if assignment.prowadzacy_id == teacher.id:
            teachersTeams.append(assignment.zespol_id)
    return teachersTeams

def getTeamsWithMembers(teacher_id: int):
    teamsIds=getTeacherTeamsIds(teacher_id)
    teamWithMembers = {team_id:teamMembers(team_id) for team_id in teamsIds}
    return teamWithMembers

def getTeamHospitalisations(team_id: int):
    hospitalisations = models.model.HospitacjaModel.getAll()
    hosps = []
    for hospitalisation in hospitalisations:
        if hospitalisation.zespol_hospitujacy_id == team_id:
            hosp = {}
            hosp["teacher"] = models.model.ProwadzacyModel.getById(hospitalisation.prowadzacy_id).fullName
            hosp["isDone"] = hospitalisation.isDone
            if hosp["isDone"]:
                hosp["isDone"] = "Zakończona"
            else:
                hosp["isDone"] = "Oczekuje"
            hosp["date"] = hospitalisation.termin
            hosps.append(hosp)
    return hosps


