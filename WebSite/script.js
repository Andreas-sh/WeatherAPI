

function Weather() {
    const Town_Name = document.getElementById('town_name');
    const Town_temp = document.getElementById('town_temp');
    const Local_time = document.getElementById('Local_time');
    const weather = document.getElementById('weather');
    const weather_img = document.getElementById('weather_pic');
    const sunrise = document.getElementById('sunrise');
    const sunset = document.getElementById('sunset');
    

    //                                     
    const inputValue = document.getElementById('towns').value;
    const city = inputValue.split(',')[0].trim();
    const country = inputValue.split(',')[1].trim();

    const apiUrl = "http://localhost:5046/WeatherForecast/GetWeatherForecast?city="+city+"&country="+country;
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const responseJson = response.json();
            console.log(responseJson);
            return responseJson;
        })
        .then(data => {
            // Display data in an HTML element
            //outputElement.textContent = JSON.stringify(data, null, 2);
            Town_Name.textContent = data['name'];
            Town_temp.textContent = data['temp'];
            Local_time.textContent = data['time'];
            weather.textContent = data["weather"];
            weather_img.src = data["weather_pic"];
            sunrise.textContent = data["sunrise"];
            sunset.textContent = data["sunset"];
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

/*initiate the autocomplete function on the "myInput" element, and pass along the countries array as possible autocomplete values:*/

function autocomplete(inp) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function(e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();

        if (!val) { return false;}

        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);

        getCities();
        
        function getCities() {
            const apiURL2 = "http://localhost:5046/WeatherForecast/GetCityNames?city="+val;
            fetch(apiURL2)
            .then(response => {
              if (!response.ok) {
                  throw new Error('Network response was not ok');
              }
  
              const responseJson = response.json();
              console.log(responseJson);
              return responseJson;
          })
          .then(data => {
            displayCities(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
        }

        function displayCities(arr) {
          /*for each item in the array...*/
          for (i = 0; i < arr.length; i++) {
              /*create a DIV element for each matching element:*/
              b = document.createElement("DIV");
              /*make the matching letters bold:*/
              b.innerHTML = "<strong>" + arr[i]["name"].substr(0, val.length) + "</strong>";
              b.innerHTML += arr[i]["name"].substr(val.length) + ", " + arr[i]["country"];
              /*insert a input field that will hold the current array item's value:*/
              b.innerHTML += "<input type='hidden' value='" + arr[i]["name"]+ ", "+ arr[i]["country"] + "'>";
              /*execute a function when someone clicks on the item value (DIV element):*/
              b.addEventListener("click", function(e) {
                  /*insert the value for the autocomplete text field:*/
                  inp.value = this.getElementsByTagName("input")[0].value;
                  /*close the list of autocompleted values,
                  (or any other open lists of autocompleted values:*/
                  closeAllLists();
              });
              a.appendChild(b);
          }
        }        
    });

    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function(e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
          /*If the arrow DOWN key is pressed,
          increase the currentFocus variable:*/
          currentFocus++;
          /*and and make the current item more visible:*/
          addActive(x);
        } else if (e.keyCode == 38) { //up
          /*If the arrow UP key is pressed,
          decrease the currentFocus variable:*/
          currentFocus--;
          /*and and make the current item more visible:*/
          addActive(x);
        } else if (e.keyCode == 13) {
          /*If the ENTER key is pressed, prevent the form from being submitted,*/
          e.preventDefault();
          if (currentFocus > -1) {
            /*and simulate a click on the "active" item:*/
            if (x) x[currentFocus].click();
          }
        }
    });

    function addActive(x) {
      /*a function to classify an item as "active":*/
      if (!x) return false;
      /*start by removing the "active" class on all items:*/
      removeActive(x);
      if (currentFocus >= x.length) currentFocus = 0;
      if (currentFocus < 0) currentFocus = (x.length - 1);
      /*add class "autocomplete-active":*/
      x[currentFocus].classList.add("autocomplete-active");
    }

    function removeActive(x) {
      /*a function to remove the "active" class from all autocomplete items:*/
      for (var i = 0; i < x.length; i++) {
        x[i].classList.remove("autocomplete-active");
      }
    }

    function closeAllLists(elmnt) {
      /*close all autocomplete lists in the document,
      except the one passed as an argument:*/
      var x = document.getElementsByClassName("autocomplete-items");
      for (var i = 0; i < x.length; i++) {
        if (elmnt != x[i] && elmnt != inp) {
          x[i].parentNode.removeChild(x[i]);
        }
      }
    }

    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
  }
  autocomplete(document.getElementById("towns"));
