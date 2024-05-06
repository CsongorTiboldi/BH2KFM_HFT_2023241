let Subjects = [];

getdata();

async function getdata() {
    await fetch('http://localhost:60321/Subject')
        .then(x => x.json())
        .then(y => {
            Subjects = y;
            console.log(Subjects);
            display();
        });

}


function display() {
    let e = document.getElementById('subjectResult');
    e.innerHTML = "";
    Subjects.forEach(t => {
        e.innerHTML += `<tr><td>${t.subjectID}</td><td>${t.name}</td><td><button onclick="removeSubject(${t.subjectID})">Delete</button></td></tr>`;
    });
}

function createSubject() {
    let inName = document.getElementById('subjectname').value;
    console.log(name);

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
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function removeSubject(id) {
    fetch(`http://localhost:60321/Subject/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}