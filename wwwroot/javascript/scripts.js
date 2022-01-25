(function () {
    window.PlTable = {
        setClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.add('mud-theme-secondary');
            document.getElementById(awayTeam).classList.add('mud-theme-secondary');
        },
        removeClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.remove('mud-theme-secondary');
            document.getElementById(awayTeam).classList.remove('mud-theme-secondary');
        }
    };

    window.Fixture = {
        setClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.add('mud-theme-secondary');
            }
        },
        removeClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.remove('mud-theme-secondary');
            }
        }
    };
})();