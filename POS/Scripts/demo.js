
const size = 20;
var canvas = document.getElementById('MainContent_cav');
var ctx = canvas.getContext("2d");
var mouse = { x: undefined, y: undefined, isClicked: false };
var grids = [];
var cg;

/// MOUSE INPUT SECTION
canvas.addEventListener("mousemove", (e) => {
    // mouse_coordinate - element_position_offset - border_width
    mouse.x = e.clientX - 70;
    mouse.y = e.clientY - 200;

});

canvas.addEventListener("mousedown", (e) => mouse.isClicked = true);

canvas.addEventListener("mouseup", (e) => mouse.isClicked = false);
/// MOUSE INPUT SECTION

function init(x,y,w,h) {

    var xs = String(x).split(",");
    var ys = String(y).split(",");
    var ws = String(w).split(",");
    var hs = String(h).split(",");


    

    for (i = 0; i < xs.length; i++) {

        grid = new Grid(xs[i], ys[i], ws[i], hs[i], size);
        grids[i] = grid;
       
    }

    animate();
    
}

// animation loop
function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
    tics = 0;
    for (i = 0; i < grids.length; i++) {
        tics  += grids[i].update(mouse, ctx);
    }
    document.getElementById('MainContent_tick').innerHTML = 'You have ' + tics + ' to book';
}

function displayCellIndex(x, y) {
    alert(x + " , " + y);
}

