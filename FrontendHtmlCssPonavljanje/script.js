"use strict";

// const employess = [
//     {
//         id: 1,
//         firstName: "Pero",
//         lastName: "Peric",
//         store: "Instar",
//         startedOn: new Date(Date.UTC(2012, 7, 15))
//             .toISOString()
//             .split("T")[0]
//             .replaceAll("-", ".") 
//     },
//     {
//         id: 2,
//         firstName: "Ana",
//         lastName: "Anic",
//         store: "Konzum",
//         startedOn: new Date(Date.UTC(2012, 7, 15))
//             .toISOString()
//             .split("T")[0]
//             .replaceAll("-", ".") 
//     },
//     {
//         id: 3,
//         firstName: "Marko",
//         lastName: "Markic",
//         store: "Konzum",
//         startedOn: new Date(Date.UTC(2012, 7, 15))
//             .toISOString()
//             .split("T")[0]
//             .replaceAll("-", ".")  
//     },
//     {
//         id: 4,
//         firstName: "Vanja",
//         lastName: "Strepic",
//         store: "Pevex",
//         startedOn: new Date(Date.UTC(2012, 7, 15))
//             .toISOString()
//             .split("T")[0]
//             .replaceAll("-", ".") 
//     }
// ]

// localStorage.setItem("employees", JSON.stringify(employess));

let mode = 0; // 0 = nothing, 1 = add, 2 = edit

function renderAllEmployees() {
    const form = document.getElementById("form");
    const table = document.getElementById("EmployeeTable");
    const employees = JSON.parse(localStorage.getItem("employees"));

    table.innerHTML = 
            `<tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Store Name</th>
                <th>Started On</th>
                <th>Actions</th>
            </tr>`;

    employees.forEach(employee => {
        table.insertAdjacentHTML("beforeend",
            `<tr>
                <td>${employee.firstName}</td>
                <td>${employee.lastName}</td>
                <td>${employee.store}</td>
                <td>${employee.startedOn}</td>
                <td>
                    <input type="button" value="Edit" name="edit" data-id="${employee.id}">
                    <input type="button" value="Delete" name="delete" data-id="${employee.id}">
                </td>
            </tr>`
        );
    });

    form.classList.add("hidden");
    mode = 0;
}

renderAllEmployees()

function deleteEmployee(id) {
    let employees = JSON.parse(localStorage.getItem("employees"));
    employees = employees.filter(el => el.id !== id);

    localStorage.setItem("employees", JSON.stringify(employees));

    renderAllEmployees();
}

function addEmployee() {
    const fName = document.getElementById("firstname");
    const lName = document.getElementById("lastname");
    const store = document.getElementById("store");
    const startedOn = document.getElementById("started");
    
    const employees = JSON.parse(localStorage.getItem("employees"));
    
    const employee = {
        id: employees.at(employees.length -1).id + 1,
        firstName: fName.value,
        lastName: lName.value,
        store: store.value,
        startedOn: startedOn.value.replaceAll("-", ".")
    }

    console.log(employee);

    employees.push(employee);

    localStorage.setItem("employees", JSON.stringify(employees));

    renderAllEmployees();
}

function updateForm(id) {
    const form = document.getElementById("form");
    const submit = document.getElementById("submit");
    const fName = document.getElementById("firstname");
    const lName = document.getElementById("lastname");
    const store = document.getElementById("store");
    const startedOn = document.getElementById("started");

    const employees = JSON.parse(localStorage.getItem("employees"));
    const employee = employees.filter(el => el.id === id)[0];

    fName.value = employee.firstName;
    lName.value = employee.lastName;
    store.value = employee.store;
    startedOn.value = employee.startedOn.replaceAll(".", "-");

    submit.dataset.employeeId = id;

    form.classList.remove("hidden");
}

function addForm() {
    const form = document.getElementById("form");
    const fName = document.getElementById("firstname");
    const lName = document.getElementById("lastname");
    const store = document.getElementById("store");
    const startedOn = document.getElementById("started");

    fName.value = "";
    lName.value = "";
    store.value = "";
    startedOn.value = "";

    form.classList.remove("hidden");
}

function editEmployee(id) {
    const form = document.getElementById("form");
    const fName = document.getElementById("firstname");
    const lName = document.getElementById("lastname");
    const store = document.getElementById("store");
    const startedOn = document.getElementById("started");

    const employees = JSON.parse(localStorage.getItem("employees"));
    const employee = employees.filter(el => el.id == id)[0];

    employee.firstName = fName.value;
    employee.lastName = lName.value;
    employee.store = store.value;
    employee.startedOn = startedOn.value.replaceAll("-", ".");

    fName.value = "";
    lName.value = "";
    store.value = "";
    startedOn.value = "";

    localStorage.setItem("employees", JSON.stringify(employees))

    form.classList.remove("hidden");

    renderAllEmployees();
}

function cancelForm() {
    const form = document.getElementById("form");
    const fName = document.getElementById("firstname");
    const lName = document.getElementById("lastname");
    const store = document.getElementById("store");
    const startedOn = document.getElementById("started");

    fName.value = "";
    lName.value = "";
    store.value = "";
    startedOn.value = "";

    form.classList.add("hidden");
}

document.getElementById("add").addEventListener("click", (e) => {
    mode = 1;
    addForm();
});

document.getElementById("EmployeeTable").addEventListener("click", (e) => {
    if (e.target.name === "delete") {
        deleteEmployee(Number(e.target.dataset.id));
    } else if (e.target.name === "edit") {
        mode = 2;
        updateForm(Number(e.target.dataset.id))
    }
});

document.getElementById("submit").addEventListener("click", (e) => {
    e.preventDefault();

    if(mode === 1) {
        addEmployee();
    } else if (mode === 2) {
        editEmployee(e.target.dataset.employeeId)
    }
})

document.getElementById("cancel").addEventListener("click", cancelForm);