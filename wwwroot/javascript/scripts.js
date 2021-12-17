(function () {
    window.PlTable = {
        setClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.add('mouse-over');
            document.getElementById(awayTeam).classList.add('mouse-over');
        },
        removeClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.remove('mouse-over');
            document.getElementById(awayTeam).classList.remove('mouse-over');
        }
    };

    window.Fixture = {
        setClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.add('mouse-over');
            }
        },
        removeClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.remove('mouse-over');
            }
        }
    };
})();