
const size = parseInt(window.SIZE);
var canvas = document.getElementById('MainContent_cav');
var ctx = canvas.getContext("2d");
var mouse = { x: undefined, y: undefined, isClicked: false };
var grids = [];
var cg;
var ready = false;


/// MOUSE INPUT SECTION
canvas.addEventListener("mousemove", mv, false);
function mv(e) {
    // mouse_coordinate - element_position_offset - border_width
    var rect = canvas.getBoundingClientRect();
    mouse.x = (e.clientX - rect.left) / (rect.right - rect.left) * canvas.width;
    mouse.y = (e.clientY - rect.top) / (rect.bottom - rect.top) * canvas.height;
    
}

canvas.addEventListener("click", (e) => mouse.isClicked = true, false);
/// MOUSE INPUT SECTION


function init(s, d, x, y, w, h, p, a, r, b) {

    for (i = 0; i < x.length; i++) {

        var ss = String(s[i]).split(";");
        ss.splice(-1)

        grid = new Grid(d[i],ss,x[i], y[i], w[i], h[i], size,p[i],a[i],r[i],b[i]);
        grids[i] = grid;
       
    }
    ready = true;
    animate();
    
}

var seats;
var cost;

// animation loop
function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
    ctx.strokeStyle = "Red";
    seats = 0;
    cost = 0;
    for (i = 0; i < grids.length; i++) {

        chairs = grids[i].update(mouse, ctx);
        for (j = 0; j < chairs.length; j++) {
            seats += chairs[j];
            cost += chairs[j] * grids[i].price[j];
        }
        
        document.getElementById('MainContent_tick').innerHTML = 'You have ' + seats + ' Tickts to book';
        document.getElementById('MainContent_price').innerHTML = 'Price: ' + cost;
        
    }
    mouse.isClicked = false;
}

function onReserve() {

    if (ready) {
        var validRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        var name = document.getElementById('MainContent_name').value;
        var mail = document.getElementById('MainContent_Mail').value;

        if (seats == 0) {
            alert("choose seate to reserve");

        }
        else if (mail.match(validRegex)) {

            var rs = [];
            var s = "";
            for (i = 0; i < grids.length; i++) {
                var seats = grids[i].toReserve();
                rs = rs.concat(seats);

            }

            for (i = 0; i < rs.length; i++) {
                s += String(rs[i].blk) + "," + String(rs[i].w) + "," + String(rs[i].h) + ";"
            }
            

            $.ajax({

                    type: 'POST',
                    url: 'Reservation.aspx/Reserve',
                    async: false,
                data: "{'n':'" + name + "','m':'" + mail + "','cells':'" + s + "','c':'" + cost + "'}",
                contentType: 'application/json; charset =utf-8',
                success: function (data) {
                    if (data.d == "error")
                        alert(mail + " is already made reservation go to edit page to change your seats");

                    else
                        alert(name + " Reserved " + seats + " Seats For " + cost + " $ at " + s);
                },
                error: function (result) {alert("Error Occured, Try Again");}
            });

            


        }
        else {

            alert("Ivalid Mail??");
        }
    }
    else {
        alert("You Should choose the event first")
    }
    
    
    
}

function displayCellIndex(x, y) {
    alert(x + " , " + y);
}

