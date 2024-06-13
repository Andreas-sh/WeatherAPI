

function Weather() {
    const Town_Name = document.getElementById('town_name');
    const Town_temp = document.getElementById('town_temp');
    const Local_time = document.getElementById('Local_time');
    const weather = document.getElementById('weather');
    const weather_img = document.getElementById('weather_pic');
    const sunrise = document.getElementById('sunrise');
    const sunset = document.getElementById('sunset');
    

                                         
    const town = document.getElementById('towns');
    const apiUrl = 'http://localhost:5046/WeatherForecast?city='+town.value;
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