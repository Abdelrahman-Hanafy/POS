
const size = parseInt(window.SIZE);
var canvas = document.getElementById('MainContent_cav');
var ctx = canvas.getContext("2d");
var mouse = { x: undefined, y: undefined, isClicked: false };
var blks = [];
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



function init(d, x, y, w, h, a, r, b) {

    for (i = 0; i < x.length; i++) {

        blk = new Block(d[i], x[i], y[i], w[i], h[i], size, a[i],r[i],b[i]);
        blks[i] = blk;

    }
    ready = true;
    animate();
}

// animation loop
function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
    ctx.strokeStyle = "Red";

    for (i = 0; i < blks.length; i++) {
        blks[i].update(mouse, ctx);
    }
    mouse.isClicked = false;
   
}

function onPricing() {


    var price = document.getElementById('MainContent_ticketPrice').value;
    for (i = 0; i < blks.length; i++) {

        for (j = 0; j < blks[i].rows.length; j++) {
            if (blks[i].rows[j].choose) {
                addPrice(blks[i].id, blks[i].rows[j].idx, price);
                blks[i].rows[j].color = "Red";
                blks[i].rows[j].choose = false;
                blks[i].rows[j].en = false;
                alert(blks[i].id + " , " + blks[i].rows[j].idx);
            }
        }
    }

}

function onPricingEdit() {


    var price = document.getElementById('MainContent_ticketPrice').value;
    for (i = 0; i < blks.length; i++) {

        for (j = 0; j < blks[i].rows.length; j++) {
            if (blks[i].rows[j].choose) {
                editPrice(blks[i].id, blks[i].rows[j].idx, price);
                blks[i].rows[j].color = "Red";
                blks[i].rows[j].choose = false;
                blks[i].rows[j].en = false;
                alert(blks[i].id + " , " + blks[i].rows[j].idx);
            }
        }
    }

}

function addPrice(id, row,price) {
    $.ajax({

        type: 'POST',
        url: 'Pricing.aspx/asg_Click',
        async: false,
        data: "{'blk':'" + id + "','row':'" + row + "','price':'" + price + "'}",
        contentType: 'application/json; charset =utf-8',
        success: function (data) {
            if (data.d == "error")
                alert("the price has been set before");

            else
                alert("Done");
        },
        error: function (result) { alert("Error Occured, Try Again"); }
    });
}

function editPrice(id, row, price) {
    $.ajax({

        type: 'POST',
        url: 'Pricing.aspx/edt_Click',
        async: false,
        data: "{'blk':'" + id + "','row':'" + row + "','price':'" + price + "'}",
        contentType: 'application/json; charset =utf-8',
        success: function (data) {
            if (data.d == "error")
                alert("the price has been set before");

            else
                alert("Done");
        },
        error: function (result) { alert("Error Occured, Try Again"); }
    });
}
