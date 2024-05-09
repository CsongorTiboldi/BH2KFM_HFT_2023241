let Subjects = [];
let Rooms = [];
let Courses = [];

let connection = null;

let subjectIDUpdate;
let roomIDUpdate;
let courseIDUpdate;

getSubjectData();
getRoomData();
getCourseData();

getSubjectNonCrud();
getRoomNonCrud();
getCourseNonCrud();

setupSiganlR();

function setupSiganlR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:60321/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    //Subjects SignalR
    connection.on("SubjectCreated", (user, message) => {
        getSubjectData();
        getSubjectNonCrud();
    });
    connection.on("SubjectDeleted", (user, message) => {
        getSubjectData();
        getSubjectNonCrud();
    });
    connection.on("SubjectUpdated", (user, message) => {
        getSubjectData();
        getSubjectNonCrud();
    });

    //Rooms SignalR
    connection.on("RoomCreated", (user, message) => {
        getRoomData();
        getRoomNonCrud();
    });
    connection.on("RoomDeleted", (user, message) => {
        getRoomData();
        getRoomNonCrud();
    });
    connection.on("RoomUpdated", (user, message) => {
        getRoomData();
        getRoomNonCrud();
    });

    //Courses SiganlR
    connection.on("CourseCreated", (user, message) => {
        getCourseData();
        getCourseNonCrud();
    });
    connection.on("CourseDeleted", (user, message) => {
        getCourseData();
        getCourseNonCrud();
    });
    connection.on("CourseUpdated", (user, message) => {
        getCourseData();
        getCourseNonCrud();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


//Non-CRUDs
async function getSubjectNonCrud() {
    let e = document.getElementById('subjectNonCrud');
    e.innerHTML = '';
    await fetch('http://localhost:60321/SubjectStat/AverageCreditValue')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>Average credit value: ${y}</li>`;
        });

    await fetch('http://localhost:60321/SubjectStat/MostCreditSemester')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>Semester with most credits: ${y}</li>`;
        });
}

async function getRoomNonCrud() {
    let e = document.getElementById('roomNonCrud');
    e.innerHTML = '';
    await fetch('http://localhost:60321/RoomStat/MaxCapacity')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>Maximal capacity: ${y}</li>`;
        });
}

async function getCourseNonCrud() {
    let e = document.getElementById('courseNonCrud');
    e.innerHTML = '';
    await fetch('http://localhost:60321/CourseStat/AverageCourseLengthMinutes')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>A course is ${y} minutes long on average</li>`;
        });

    await fetch('http://localhost:60321/CourseStat/MaxCourseLengthMinutes')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>The longest course is ${y} minutes long</li>`;
        });

    await fetch('http://localhost:60321/CourseStat/AnyOverlapping')
        .then(x => x.json())
        .then(y => {
            e.innerHTML += `<li>Are there overlapping courses: ${y}</li>`;
        });
}

//Read
async function getSubjectData() {
    await fetch('http://localhost:60321/Subject')
        .then(x => x.json())
        .then(y => {
            Subjects = y;
            displaySubjects();
            console.log(Subjects);
        });
}

async function getRoomData() {
    await fetch('http://localhost:60321/Room')
        .then(x => x.json())
        .then(y => {
            Rooms = y;
            displayRooms();
            console.log(Rooms);
        });
}

async function getCourseData() {
    await fetch('http://localhost:60321/Course')
        .then(x => x.json())
        .then(y => {
            Courses = y;
            displayCourses();
            console.log(Courses);
        });
}


function displaySubjects() {
    let e = document.getElementById('subjectResult');
    e.innerHTML = "";
    Subjects.forEach(t => {
        e.innerHTML +=
            `<tr>
                <td>${t.subjectID}</td>
                <td>${t.name}</td>
                <td>${t.credits}</td>
                <td>${t.semester}</td>
                <td>
                    <button onclick="removeSubject(${t.subjectID})">Delete</button>
                    <button onclick="showSubjectEditor(${t.subjectID})">Edit</button>
                </td>
            </tr>`;
    });
}

function displayRooms() {
    let e = document.getElementById('roomResult');
    e.innerHTML = "";
    Rooms.forEach(t => {
        e.innerHTML +=
            `<tr>
                <td>${t.doorID}</td>
                <td>${t.capacity}</td>
                <td>${t.hasProjector}</td>
                <td>
                    <button onclick="removeRoom(${t.doorID})">Delete</button>
                    <button onclick="showRoomEditor(${t.doorID})">Edit</button>
                </td>
            </tr>`;
    });
}

function displayCourses() {
    let e = document.getElementById('courseResult');
    e.innerHTML = "";
    Courses.forEach(t => {
        e.innerHTML +=
            `<tr>
                <td>${t.courseID}</td>
                <td>${t.location}</td>
                <td>${t.startTime}</td>
                <td>${t.endTime}</td>
                <td>${t.courseSubject}</td>
                <td>
                    <button onclick="removeCourse(${t.courseID})">Delete</button>
                    <button onclick="showCourseEditor(${t.courseID})">Edit</button>
                </td>
            </tr>`;
    });
}


//Update
function showSubjectEditor(id) {
    document.getElementById('subjectUpdateForm').style.display = 'block';
    let obj = Subjects.find(t => t['subjectID'] == id);
    document.getElementById('subjectnameupdate').value = obj['name'];
    document.getElementById('subjectcreditupdate').value = obj['credits'];
    document.getElementById('subjectsemesterupdate').value = obj['semester'];
    subjectIDUpdate = id;
}

function showRoomEditor(id) {
    document.getElementById('roomUpdateForm').style.display = 'block';
    let obj = Rooms.find(t => t['doorID'] == id);
    document.getElementById('roomcapacityupdate').value = obj['capacity'];
    document.getElementById('roomprojectorupdate').checked = obj['hasProjector'];
    roomIDUpdate = id;
}

function showCourseEditor(id) {
    document.getElementById('courseUpdateForm').style.display = 'block';
    let obj = Courses.find(t => t['courseID'] == id);
    document.getElementById('courselocationupdate').value = obj['location'];
    document.getElementById('coursestartupdate').value = obj['startTime'];
    document.getElementById('courseendupdate').value = obj['endTime'];
    document.getElementById('coursesubjectupdate').value = obj['courseSubject'];
    courseIDUpdate = id;
}


function updateSubject() {
    document.getElementById('subjectUpdateForm').style.display = 'none';
    let inName = document.getElementById('subjectnameupdate').value;
    let inCredit = Number(document.getElementById('subjectcreditupdate').value);
    let inSemester = Number(document.getElementById('subjectsemesterupdate').value);
    fetch('http://localhost:60321/Subject', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            subjectID: subjectIDUpdate,
            name: inName,
            credits: inCredit,
            semester: inSemester
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getSubjectData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function updateRoom() {
    document.getElementById('roomUpdateForm').style.display = 'none';
    let inCapacity = Number(document.getElementById('roomcapacityupdate').value);
    let inHasProjector = document.getElementById('roomprojectorupdate').checked;
    fetch('http://localhost:60321/Room', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            doorID: roomIDUpdate,
            capacity: inCapacity,
            hasProjector: inHasProjector
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRoomData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function updateCourse() {
    document.getElementById('courseUpdateForm').style.display = 'none';
    let inLocation = document.getElementById('courselocationupdate').value;
    let inStartTime = document.getElementById('coursestartupdate').value;
    let inEndTime = document.getElementById('courseendupdate').value;
    let inCourseSubject = document.getElementById('coursesubjectupdate').value;
    fetch('http://localhost:60321/Course', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            courseID: courseIDUpdate,
            location: inLocation,
            startTime: inStartTime,
            endTime: inEndTime,
            courseSubject: inCourseSubject
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getSubjectData();
        })
        .catch((error) => { console.error('Error:', error); });
}


function updateSubjectCancel() {
    document.getElementById('subjectUpdateForm').style.display = 'none';
}

function updateRoomCancel() {
    document.getElementById('roomUpdateForm').style.display = 'none';
}

function updateCourseCancel() {
    document.getElementById('courseUpdateForm').style.display = 'none';
}


//Create
function createSubject() {
    let inName = document.getElementById('subjectname').value;
    document.getElementById('subjectname').value = "";

    //console.log(name);

    fetch('http://localhost:60321/Subject', {
        method: 'POST',
        headers: {'Content-Type': 'application/json',},
        body: JSON.stringify({
            name: inName
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getSubjectData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createRoom() {
    let inCapacity = Number(document.getElementById('roomcapacity').value);
    document.getElementById('roomcapacity').value = "";

    fetch('http://localhost:60321/Room', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            capacity: inCapacity
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRoomData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createCourse() {
    let inLocation = Number(document.getElementById('courselocation').value);
    document.getElementById('courselocation').value = "";

    fetch('http://localhost:60321/Course', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            location: inLocation
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCourseData();
        })
        .catch((error) => { console.error('Error:', error); });
}


//Delete
function removeSubject(id) {
    fetch(`http://localhost:60321/Subject/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getSubjectData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function removeRoom(id) {
    fetch(`http://localhost:60321/Room/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRoomData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function removeCourse(id) {
    fetch(`http://localhost:60321/Course/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCourseData();
        })
        .catch((error) => { console.error('Error:', error); });
}