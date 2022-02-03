from flask import *

import models.model
from models.utils import getProcessedHospitalisations
app = Flask(__name__)



@app.route('/', methods=["GET","POST"])
def home():
    return render_template("main.html")


@app.route('/principalMenu', methods=["GET", "POST"])
def principalMenu():  # put application's code here
    return render_template("index.html")


@app.route('/hospitalisationsPage', methods=["GET", "POST"])
def hospitalisationsPage():
    return render_template("zarzadzaniePlanem.html", hospitalisations=getProcessedHospitalisations())




@app.route('/addHospitalisationPage', methods=["GET", "POST"])
def addHospitalisationPage():
    if request.method == "POST":
        teachers = models.utils.getTeachersWithCourse()
        teams = models.model.ZespolHospitujacyModel.getAll()
        return render_template("dodajHospitacje.html", teachers=teachers, teams=teams)

@app.route('/addHospitalisationAction', methods=["GET", "POST"])
def addHospitalisationAction():
    if request.method == "POST":
        teacher_id = int(request.form["teacher_id"])
        team_id = int(request.form["team_id"])
        models.model.HospitacjaModel.add(models.model.Hospitacja(prowadzacy_id=teacher_id ,zespol_hospitujacy_id=team_id))
        teachers = models.utils.getTeachersWithCourse()
        teams = models.model.ZespolHospitujacyModel.getAll()
        return render_template("dodajHospitacje.html", result="Pomyślnie dodano hospitację!", teachers=teachers, teams=teams)




@app.route('/hospitalisationDetailsPage', methods=["GET", "POST"])
def hospitalisationDetailsPage():
    if request.method == "POST":
        hospitalisation = models.model.HospitacjaModel.getById(int(request.form["id"])).toDict()
        if hospitalisation["isDone"]:
            hospitalisation["isDone"] = "Zakończona"
        else:
            hospitalisation["isDone"] = "Oczekuje"
        hospitalisation["teacher"] = request.form["teacher"]
        teams = models.model.ZespolHospitujacyModel.getAll()
        courses = models.utils.getDictTeacherCourses()[hospitalisation["prowadzacy_id"]]
        return render_template("szczegolyPlan.html", hospitalisation=hospitalisation, teams=teams, courses=courses)


@app.route('/deleteHospitalisation', methods=["GET", "POST"])
def deleteHospitalisation():
    if request.method == "POST":
        hospId = int(request.form["id"])
        # czy można usunąć przeprowadzone hospitacje???
        hosp = models.model.HospitacjaModel.getById(hospId)
        if not hosp.isDone:
            models.model.HospitacjaModel.delete(hospId)
            return render_template("zarzadzaniePlanem.html", result="Pomyślnie usunięto hospitację!",
                                   hospitalisations=getProcessedHospitalisations())
        else:
            return render_template("zarzadzaniePlanem.html", result="Nie można usunąć zakończonej hospitacji!",
                               hospitalisations=getProcessedHospitalisations())

@app.route('/updateHospitalisationAction', methods=["GET", "POST"])
def updateHospitalisationAction():
    if request.method == "POST":
        hosp_id = int(request.form["id"])
        team_id = int(request.form["team_id"])
        course_code = request.form["course_code"]

        models.model.HospitacjaModel.update(hosp_id, zespol_hospitujacy_id=team_id, kurs_kod=course_code)
        # return render_template("zarzadzaniePlanem.html", hospitalisations=getProcessedHospitalisations())
        hospitalisation = models.model.HospitacjaModel.getById(hosp_id).toDict()
        if hospitalisation["isDone"]:
            hospitalisation["isDone"] = "Zakończona"
        else:
            hospitalisation["isDone"] = "Oczekuje"
        hospitalisation["teacher"] = request.form["teacher"]
        hospitalisation["team_id"] = request.form["team_id"]
        teams = models.model.ZespolHospitujacyModel.getAll()
        courses = models.utils.getDictTeacherCourses()[hospitalisation["prowadzacy_id"]]
        return render_template("szczegolyPlan.html", hospitalisation=hospitalisation, teams=teams,
                               courses=courses, result="Pomyślnie zaktualizowano hospitację!")



@app.route('/memberMenu')
def memberMenu():
    teams = models.utils.getTeamsWithMembers(1)
    return render_template("menuCzlonka.html", teams=teams)

@app.route('/teamDetails', methods=["GET", "POST"])
def teamDetails():
    if request.method == "POST":
        team_id = int(request.form["id"])
        members = models.utils.teamMembers(team_id)
        hospitalisations = models.utils.getTeamHospitalisations(team_id)
        return render_template("szczegolyZespol.html", team_id=team_id, members=members, hospitalisations=hospitalisations)


@app.route('/memberIndex')
def memberIndex():
    return render_template("memberIndex.html")




if __name__ == '__main__':

    # app = create_app()

    app.debug = True
    app.run()
