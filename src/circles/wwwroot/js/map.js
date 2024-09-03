window.mapInterop = {
    loadGoogleMaps: function (apiKey) {
        return new Promise((resolve, reject) => {
            if (typeof google !== 'undefined' && typeof google.maps !== 'undefined') {
                resolve();
                return;
            }

            const script = document.createElement('script');
            script.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}`;
            script.async = true;
            script.onload = () => {
                if (typeof google !== 'undefined' && typeof google.maps !== 'undefined') {
                    console.log("Google Maps API loaded successfully.");
                    resolve();
                } else {
                    console.error("Google Maps API failed to load correctly.");
                    reject(new Error("Google Maps API failed to load."));
                }
            };
            script.onerror = () => {
                console.error("Error loading Google Maps API.");
                reject(new Error("Google Maps API failed to load."));
            };
            document.head.appendChild(script);
        });
    },

    initMap: function () {
        if (typeof google === 'undefined' || typeof google.maps === 'undefined') {
            console.error("Google Maps API is not loaded.");
            return;
        }

        window.map = new google.maps.Map(document.getElementById("map"), {
            center: { lat: 50, lng: 0 },
            zoom: 5,
        });

        console.log("Map initialized successfully.");
    },

    updateMap: function (latitude, longitude) {
        if (!window.map) {
            console.error("Map object is not initialized.");
            return;
        }

        const location = { lat: latitude, lng: longitude };
        window.map.setCenter(location);
        window.map.setZoom(10); 
        new google.maps.Marker({
            position: location,
            map: window.map
        });

        console.log("Map updated to new location.");
    }
};
