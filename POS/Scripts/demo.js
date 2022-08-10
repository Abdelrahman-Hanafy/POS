
const size = 20;
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


function init(x,y,w,h,p) {

    var xs = String(x).split(",");
    var ys = String(y).split(",");
    var ws = String(w).split(",");
    var hs = String(h).split(",");
    var ps = String(p).split(",");

    for (i = 0; i < xs.length; i++) {

        grid = new Grid(xs[i], ys[i], ws[i], hs[i], size,ps[i]);
        grids[i] = grid;
       
    }
    ready = true;
    animate();
    
}

var tics;
var sum;

// animation loop
function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
    ctx.strokeStyle = "Red";
    tics = 0;
    sum = 0 
    for (i = 0; i < grids.length; i++) {

        ctx.strokeRect(grids[i].x, grids[i].y, grids[i].row_count * size, grids[i].column_count * size);
        chairs = grids[i].update(mouse, ctx);
        tics += chairs;
        sum += chairs * grids[i].price;
        document.getElementById('MainContent_tick').innerHTML = 'You have ' + tics + ' Tickts to book';
        document.getElementById('MainContent_price').innerHTML = 'Price: ' + sum;
        
    }
    //PageMethods.Show("here");
    mouse.isClicked = false;
}

function onReserve() {
    if (ready) {
        var validRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        var name = document.getElementById('MainContent_name').value;
        var mail = document.getElementById('MainContent_Mail').value;

        if (tics == 0) {
            alert("choose seate to reserve");

        }
        else if (mail.match(validRegex)) {

            var rs = [];
            var s = "";
            for (i = 0; i < grids.length; i++) {
                rs = rs.concat( grids[i].toReserve());

            }

            for (i = 0; i < rs.length; i++) {
                s += String(rs[i].w) + "," + String(rs[i].h) + "; "
            }
            

            $.ajax({

                    type: 'POST',
                    url: 'Reservation.aspx/Reserve',
                    async: false,
                    data: "{'n':'" + name + "','m':'" + mail + "','c':'" + sum + "'}",
                contentType: 'application/json; charset =utf-8',
                success: function (data) { alert(name + " Reserved " + tics+" Seats For "+sum+" $ at " + s); },
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

