
const size = 20;
var canvas = document.getElementById('MainContent_cav');
var ctx = canvas.getContext("2d");
var mouse = { x: undefined, y: undefined, isClicked: false };
var grids = [];
var cg;

/// MOUSE INPUT SECTION
canvas.addEventListener("mousemove", mv, false);

function mv(e) {
    // mouse_coordinate - element_position_offset - border_width
    mouse.x = e.clientX - 70;
    mouse.y = e.clientY - 200;
    
}


//canvas.addEventListener("mousedown", (e) => mouse.isClicked = true, false);
//canvas.addEventListener("mouseup", (e) => mouse.isClicked = false, false);
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

    animate();
    
}

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
        document.getElementById('MainContent_tick').innerHTML = 'You have ' + tics + ' to book      Price: ' + sum;
        
    }
    mouse.isClicked = false;
    }
    

function displayCellIndex(x, y) {
    alert(x + " , " + y);
}

