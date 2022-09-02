// podaci od interesa
var host = "https://localhost:";
var port = "44352/";
var adsEndpoint = "api/oglasi/";
var agenciesEndpoint = "api/agencije/"
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
//var statisticsEndpoint = "api/statistics";
var searchEndpoint = "api/pretraga"
//var productsByCategoryEndpoint = "api/proizvodiPoKategoriji"
var formAction = "Create";
var editingId;
var categoryForFilter = "categoryFilterDropDown";
var categoryForForm = "adAgency";
var jwt_token;
var headers = { 'Content-Type': 'application/json' };

function setLogRegButtonsPage(){
	document.getElementById("logRegButtons").style.display = "flex";
	document.getElementById("logout").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";		//ako su tabele onda flex
	document.getElementById("data").style.display = "block";
	document.getElementById("search").style.display = "none";
	document.getElementById("formDiv").style.display = "none";
	getDataForTable();
	
}

function setRegistrationPage(){
	document.getElementById("logRegButtons").style.display = "none";
	document.getElementById("registerForm").reset();
	document.getElementById("logout").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "block";		//ako su tabele onda flex
	//document.getElementById("data").style.display = "block";
	//document.getElementById("search").style.display = "none";
	//document.getElementById("formDiv").style.display = "none";
}

function setLoginPage(){
	document.getElementById("logRegButtons").style.display = "none";
	document.getElementById("loginForm").reset();
	document.getElementById("logout").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "flex";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("categoryFilter").style.display = "none";
	document.getElementById("data").style.display = "block";
	document.getElementById("search").style.display = "none";
	document.getElementById("enterObjectForm").reset(); 
	document.getElementById("formDiv").style.display = "none";
	getDataForTable();
}

function setWorkPage(){
	document.getElementById("logRegButtons").style.display = "none";
	document.getElementById("logout").style.display = "flex";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("data").style.display = "block";
	//document.getElementById("categoryFilter").style.display = "block";
	//getDataForDropdown(categoryForFilter);

	//document.getElementById("search").style.display = "block";
	document.getElementById("enterObjectForm").reset(); 
	document.getElementById("formDiv").style.display = "block";
	getDataForTable();
	getDataForDropdown(categoryForForm);		//argument je id select elementa u formi (definisan u podacima od interesa)
}

function getDataForTable(){
	var requestUrl = host + port + adsEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(fillTable);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}

function fillTable(data){
	var container = document.getElementById("data");
	container.innerHTML = "";
	console.log(data);

	// ispis naslova
	var div = document.createElement("div");
	var h3 = document.createElement("h3");
	h3.className = "text-center";
	var headingText = document.createTextNode("Oglasi za nekretnine");
	h3.appendChild(headingText);
	div.appendChild(h3);

	// ispis tabele
	var table = document.createElement("table");
	table.className = "table table-bordered";
	var header = createHeader();
	table.append(header);

	var tableBody = document.createElement("tbody");

	for (var i = 0; i < data.length; i++)
	{
		// prikazujemo novi red u tabeli
		var row = document.createElement("tr");
		row.className = "text-center"								//formatiranje tabele
		// prikaz podataka
		row.appendChild(createTableCell(data[i].name));
		row.appendChild(createTableCell(data[i].estatePrice));
		row.appendChild(createTableCell(data[i].estateType));
		row.appendChild(createTableCell(data[i].agencyName));
		if(jwt_token){
			row.appendChild(createTableCell(data[i].yearConstructed));

			var stringId = data[i].id.toString();
			//dugme za brisanje
			var buttonDeleteCell = document.createElement("td");
			var buttonDelete = document.createElement("button");
			buttonDelete.setAttribute('class', 'btn btn-link');
			buttonDelete.name = stringId;
			buttonDelete.addEventListener("click", deleteObject);
			buttonDelete.textContent = 'Obrisi';
			buttonDeleteCell.appendChild(buttonDelete);
			row.appendChild(buttonDeleteCell);

			//dugme za izmenu
			// var buttonEditCell = document.createElement("td");
			// var buttonEdit = document.createElement("button");
			// buttonEdit.setAttribute('class', 'btn btn-link');
			// buttonEdit.name = stringId;
			// buttonEdit.addEventListener("click", editObject);
			// buttonEdit.textContent = 'Izmeni';
			// buttonEditCell.appendChild(buttonEdit);
			// row.appendChild(buttonEditCell);
		}
		tableBody.appendChild(row);		
	}

	table.appendChild(tableBody);
	div.appendChild(table);

	// ispis novog sadrzaja
	container.appendChild(div);
}
function createHeader(){
    var thead = document.createElement("thead");
	var row = document.createElement("tr");
    row.className = "text-center";	
    row.style = "font-weight: bold; background-color: yellow;";
	row.appendChild(createTableCell("Naslov"));
	row.appendChild(createTableCell("Cena"));
	row.appendChild(createTableCell("Tip nekretnine"));
	row.appendChild(createTableCell("Agencija"));

	if(jwt_token){
	row.appendChild(createTableCell("Godina izgradnje"));
	row.appendChild(createTableCell("Akcija"));
	}
	thead.appendChild(row);
	return thead;
}
function createTableCell(text) {
	var cell = document.createElement("td");
	var cellText = document.createTextNode(text);
	cell.appendChild(cellText);
	return cell;
}

/* Pribavljanje podataka za dropdown listu */
function getDataForDropdown(text){	//u text se prosledjuje id select elementa, moglo bi i bez toga ako je samo jedan dropdown u aplikaciji
	console.log(text);
	var requesturl = host + port + agenciesEndpoint;
    console.log(requesturl);
    var headers = { };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
    fetch(requesturl,{ headers: headers })
    .then((response) => {if(response.status === 200){
        response.json()
		.then((data) => {
			console.log(data, text);
			fillDropdown(data, text);
		});
    }else{
        console.log(response.status);
    }
    })
    .catch(error => console.log(error));
}
function fillDropdown(data, text){
	console.log(data);
    var select = document.getElementById(text);
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++){
        var opt = document.createElement('option');
        opt.value = data[i].id;
        opt.innerHTML = data[i].name;
        select.appendChild(opt);
    }
}

/*Slanje forme za filtriranje proizvoda preko dropdowna. 
Zatim se poziva metod za popunjavanje tabele dobijenim podacima*/
function submitCategoryForm(){
	var categoryId = document.getElementById(categoryForFilter).value;
	var requesturl = host + port + productsByCategoryEndpoint + "?categoryId=" + categoryId;
    console.log(requesturl);
    var headers = { };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
    fetch(requesturl,{ headers: headers })
    .then((response) => {if(response.status === 200){
        response.json()
		.then((data) => {
			console.log(data);
			fillTable(data);
		});
    }else{
        console.log(response.status);
    }
    })
    .catch(error => console.log(error));
	return false;
}

//	slanje forme za unos novog ili izmenu postojeceg objekta
function submitObjectForm(){
    var adName = document.getElementById("adName").value;
    var adPrice = document.getElementById("adPrice").value;
    var adType = document.getElementById("adType").value;
    var adYear = document.getElementById("adYear").value;
    var adAgency = document.getElementById("adAgency").value;

    // if (formAction == "Update"){
    //     var method = "PUT";
    //     var requestUrl = host + port + sellersEndpoint + editingId;
    //     var sendData = {"id": editingId, "name": sellerName, "surname": sellerSurname, "yearOfBirth": sellerYearOfBirth, "shopId": sellerShop}
	// 	console.log(sendData);
    // }else{
        var method = "POST";
        var requestUrl = host + port + adsEndpoint;
        var sendData = {"name": adName, "estateType": adType, "yearConstructed": adYear, "estatePrice": adPrice, "agencyId": adAgency};
    //}

    if(validateObjectForm(adName, adType, adYear, adPrice)){
        var headers = {
            "Authorization": "Bearer " + jwt_token,
            "Content-Type": "application/json"
        }
        fetch(requestUrl, {method: method, headers: headers, body: JSON.stringify(sendData)})
        .then(response => {
            if(response.status === 200 || response.status === 201){
                console.log("Zahtev uspesno izvrsen!");
                alert("Zahtev uspesno izvrsen!");
                setWorkPage();
            }else{
                console.log("Error occured with code " + response.status);
                console.log(response);
                alert("Desila se greska!");
				setWorkPage();
            }
        })
        .catch(error => console.log(error));
    };
    return false;
}
function validateObjectForm(adName, adType, adYear, adPrice){
    if(!adName || !adType || !adYear || !adPrice){
        alert("Data not entered correctly!");
        return false;
    }
    return true;
}

//ovo je potrebno prilikom odustajanja od editovanja objekta kad istu formu koristimo i za unos i za edit
function clearForm(){                       
    formAction = "Create";
    getDataForDropdown(shop);
}

//ovo je za sakrivanje forme za edit kada se pritisne dugme za reset, ako je tako specificirano
function hideForm(){
	document.getElementById("formDiv").style.display = "none";
		setWorkPage();
}

/*Salje se zahtev iz Search forme */
function submitSearchForm(){
    var maxCaps = document.getElementById("maxCaps").value;
    var minCaps = document.getElementById("minCaps").value;
	var method = "POST";
	var requestUrl = host + port + searchEndpoint;
	var sendData = {"start": minCaps, "end": maxCaps};
	if(validateSearchForm(minCaps, maxCaps)){
        var headers = {
            "Authorization": "Bearer " + jwt_token,
            "Content-Type": "application/json"
        }
        fetch(requestUrl, {method: method, headers: headers, body: JSON.stringify(sendData)}) //, body: JSON.stringify(sendData)
        .then(response => {
			if(response.status === 200){
				response.json()
				.then(data => {
					console.log(data);
					document.getElementById("searchForm").reset();
					fillTable(data);
				})
			}
			else if(response.status === 204){
				alert("Nema podataka za zadatu pretragu!");
				setWorkPage();
			}
			else{
				alert("Desila se greska!");
				setWorkPage();
			}
		})
        .catch(error => console.log(error));
    };
    return false;
}
function validateSearchForm(minCaps, maxCaps){
	if(!minCaps || !maxCaps || minCaps<1 || maxCaps<1 || minCaps>999 || maxCaps>999 || parseInt(maxCaps) < parseInt(minCaps)){
        alert("Greska prilikom pretrage!");
        return false;
    }
    return true;
}

/* Kada se klikne na dugme delete u tabeli */
function deleteObject(){
	var deleteId = this.name;
	var requestUrl = host + port + adsEndpoint + deleteId;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	fetch(requestUrl, { method: "DELETE", headers: headers })
		.then((response) => {
			if (response.status === 200) {
                alert("Brisanje uspesno izvrseno!")
				setWorkPage();
			} else {
				console.log("Error occured with code " + response.status);
                alert("Doslo je do greske!");
				setWorkPage();
			}
		})
		.catch(error => console.log(error));
}

//Kada se klikne na dugme edit u tabeli
function editObject(){
	//document.getElementById("formDiv").style.display = "block";	// ovde jer se forma otvara tek pri pritisku dugmeta edit
	//getDataForDropdown("team");	//ovo ne treba ovde ako je forma stalno prisutna

	formAction = "Update";	//menja se namena forme - umesto unosa novog objekta, sada je za izmenu postojeceg
	editingId = this.name;
	/* Sledi GET zahtev za dobavljanje objekta od APi-ja */
	var requestUrl = host + port + sellersEndpoint + editingId;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then((data) => {
					document.getElementById("sellerName").value = data.name;
					document.getElementById("sellerSurname").value = data.surname;
					document.getElementById("sellerYearOfBirth").value = data.yearOfBirth;
					document.getElementById("sellerShop").value = data.shopId;
				})
			} else {
				console.log("Error occured with code " + response.status);
                alert("Doslo je do greske!");
				setWorkPage();
			}
		})
		.catch(error => console.log(error));
}




function registerUser(){
    var email = document.getElementById("emailRegister").value;
    var username = document.getElementById("usernameRegister").value;
    var password = document.getElementById("passwordRegister").value;
    var confirmPassword = document.getElementById("confirmPasswordRegister").value;
    if(validateRegisterForm(email, username, password, confirmPassword)){
        var requestUrl = host + port + registerEndpoint;
        var sendData = {"email": email, "username": username, "password": password};
        fetch(requestUrl, {method: "POST", headers: {'Content-Type':'application/json'}, body: JSON.stringify(sendData)})
        .then(response => {
            if(response.status === 200){
                console.log("Successful registration");
                alert("Successful registration");
                console.log(data);
				setLoginPage();
            }else{
                console.log("Error occured with code " + response.status);
                console.log(response);
                alert("Desila se greska!");
				setRegistrationPage();
            }
        })
        .catch(error => console.log(error));
    };
    return false;
}
function validateRegisterForm(email, username, password, confirmPassword){
    if(!email || !username || !password || !confirmPassword){
        alert("Data not entered correctly!");
        return false;
    }else if(password !== confirmPassword){
        alert("Password not entered correctly!");
		return false;
    }
    return true;
}

function loginUser(){
    var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					//alert("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Prijavljeni korisnik: <b>" + data.username + "<b/>";
						jwt_token = data.token;
						setWorkPage();
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}
function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}
function logout(){
	jwt_token = undefined;
	//setLoginPage();
	setLogRegButtonsPage();
}

