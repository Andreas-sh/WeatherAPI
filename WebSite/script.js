

function Weather() {
    var Town_Name = document.getElementById('town_name');
    var Town_temp = document.getElementById('town_temp');
    var Local_time = document.getElementById('Local_time');
    var weather = document.getElementById('weather');
    var weather_img = document.getElementById('weather_pic');
    var sunrise = document.getElementById('sunrise');
    var sunset = document.getElementById('sunset');
    

    //                                     
    var inputValue = document.getElementById('towns').value;
    var city = inputValue.split(',')[0].trim();
    var country = inputValue.split(',')[1].trim();
    let bodyImg = document.body;

    var apiUrl = "http://localhost:5046/WeatherForecast/GetWeatherForecast?city="+city+"&country="+country;
    // fetch(apiUrl)
    //     .then(response => {
    //         if (!response.ok) {
    //             throw new Error('Network response was not ok');
    //         }

    //         var responseJson = response.json();
    //         console.log(responseJson);
    //         return responseJson;
    //     })
    //     .then(data => {
    //         // Display data in an HTML element
    //         //outputElement.textContent = JSON.stringify(data, null, 2);
    //         Town_Name.textContent = data['name'];
    //         Town_temp.textContent = data['temp'];
    //         Local_time.textContent = data['time'];
    //         weather.textContent = data["weather"];
    //         weather_img.src = data["weather_pic"];
    //         sunrise.textContent = data["sunrise"];
    //         sunset.textContent = data["sunset"];
    //     })
    //     .catch(error => {
    //         console.error('Error:', error);
    //     });
        fetch(apiUrl)
        .then(response => {
          response.json().then(data => {
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
      })
        .catch(error => {
            console.error('Error:', error);
        });
  if(weather.value == "Sunny")
    {
      bodyImg.style.backgroundImage="https://imengine.public.prod.cmg.infomaker.io/?uuid=84860344-c8d8-51ee-b191-943a4ff8b68d&function=cropresize&type=preview&source=false&q=75&crop_w=0.99999&crop_h=0.99999&width=1200&height=675&x=1.0E-5&y=1.0E-5";
    }
  else if(weather.value == "Clear")
    {
      bodyImg.style.backgroundImage="https://www.shutterstock.com/image-photo/full-moon-background-600nw-128504084.jpg";
    }
  else if(weather.value == "Partly cloudy")
    {
      bodyImg.style.backgroundImage="https://s7d2.scene7.com/is/image/TWCNews/clouds_from_above";
    }
  else if(weather.value == "Cloudy" || weather.value == "Overcast" || weather.value == "Patchy rain possible")
    {
      bodyImg.style.backgroundImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZIo-cJKTC0Lf2GqXctG8m4zQIdXwUYYL17A&s";
    }
  else if(weather.value == "Mist")
    {
      bodyImg.style.backgroundImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQtxM-KXFCJXoBDh9NyVD4X1kVR1nPQBiQgzg&s";
    }
  else if(weather.value == "Patchy snow possible")
    {
      bodyImg.style.backgroundImage="https://www.shutterstock.com/image-photo/rolling-hill-evenly-patchy-snow-260nw-1667981188.jpg";
    }
  else if(weather.value == "Patchy sleet possible")
    {
        bodyImg.style.backgroundImage="https://www.theweatheroutlook.com/userpics/20201227/20201227161150.jpg";
    }
  else if(weather.value == "Patchy freezing drizzle possible")
    {
        bodyImg.style.backgroundImage="https://www.pressherald.com/wp-content/uploads/sites/4/2018/01/Icy-roads.jpg?w=750";
    }
  else if(weather.value == "Thundery outbreaks possible")
    {
        bodyImg.style.backgroundImage="https://survive-a-storm.com/wp-content/media/sites/60/2017/03/SAS-Blog-Spring-07.jpg";
    }
  else if(weather.value == "Blowing snow")
    {
        bodyImg.style.backgroundImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiZquEiKlDnqBcQ6fetxgLDxQIDlArOHVBMw&s";
    }
  else if(weather.value == "Blizzard")
    {
        bodyImg.style.backgroundImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfKaDWI1vAUg7LQjyaJHAwnrzMqrJ3p_kzmA&s";
    }
  else if(weather.value == "Fog" || weather.value == "Freezing fog")
    {
       bodyImg.style.backgroundImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSQI7GVAaDiN5Er8wTYAR6YaHSVtu4gRZbxYw&s";
    }
  else if(weather.value == "Patchy light drizzle" || weather.value == "Light drizzle")
    {
       bodyImg.style.backgroundImage="https://www.abergavennychronicle.com/tindle-static/image/2023/08/18/6/shutterstock_2258369673.jpg?trim=0,22,0,312&width=669&height=445&crop=669:445";
    }
  else if(weather.value == "Freezing drizzle" || weather.value == "Heavy freezing drizzle")
    {
      bodyImg.style.backgroundImage="https://smartpilot.ca/images/freezing_drizzle_1.png";
    }
  else if(weather.value == "Patchy light rain" ||  weather.value == "Light rain")
    {
      bodyImg.style.backgroundImage="https://i.pinimg.com/736x/19/cc/ed/19ccedbb63ab75218ea516c82ddd84f0.jpg";
    }
  else if(weather.value == "Moderate rain at times" || weather.value == "Moderate rain" )
    {
      bodyImg.style.backgroundImage="https://myrepublica.nagariknetwork.com/uploads/media/rain_20210802140558.jpg";
    }
  
  
  
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
