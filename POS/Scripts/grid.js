
class Grid {

	constructor(d,b,X,Y,row_count, column_count, cell_size,P) {

		this.id = d;
		this.row_count = row_count;
		this.column_count = column_count;
		this.cell_size = cell_size;
		this.x = X;
		this.y = Y;
		this.price = P;
		this.cells = [];
		this.booked = b;
		
		for(var x=0; x<this.row_count; x++) {
			var row = [];
			for (var y = 0; y < this.column_count; y++) {
				var c = new Cell(this.id, this.x, this.y, x, y, cell_size);
				if (this.booked.includes(x+","+y)) c.book();
				row.push(c)
			}
			this.cells.push(row);
		}
	}

	update(mouse, ctx) {
		var count = 0;

		if (this.cells.length < 1) {
			ctx.strokeRect(this.x, this.y, 250, 50);
			return count;
        }

		for(var x=0; x<this.row_count; x++) {
			for(var y=0; y<this.column_count; y++) {
				var c = this.cells[x][y];
				
				if (c.choose) count++;

				if ((c.x <= mouse.x) && (mouse.x < c.x+c.size) && 
					(c.y <= mouse.y) && (mouse.y < c.y + c.size)) {
					if (!c.booked) c.onHover();
					

					if (c.booked) {

                    }
					else if (!c.choose && mouse.isClicked) {
						c.onClick("Green");
					}
					else if (c.choose && mouse.isClicked) {
						c.onClick("Blue");
					}
						
				}
				else c.onUnhover();

				c.draw(ctx)
				
			}
		}
		return count;
	}

	toReserve() {
		var rs = [];
		for (var x = 0; x < this.row_count; x++) {
			for (var y = 0; y < this.column_count; y++) {
				var c = this.cells[x][y];
				if (c.choose) {
					c.book();
					rs.push(c);
				}
			}
		}
		return rs;
    }
}


class Cell {

	constructor(d,X,Y,x_index, y_index, size) {
		this.padding = 1;
		this.size = size;
		this.blk = d;
		this.w = x_index;
		this.h = y_index;
		this.x = Number(X) + Number(x_index * this.size);
		this.y = Number(Y) + Number(y_index * this.size);
		this.color = "Blue"; // default is white
		this.isHovered = false;
		this.booked = false;
		this.choose = false;
	}

	onHover() { this.isHovered = true; }
	onUnhover() { this.isHovered = false; }

	onClick(color) {

		this.choose = !this.choose;
		this.color = color
		
	}

	book() {
		this.booked = true;
		this.color = "Red";
    }

	draw(ctx) {

		// Rotated rectangle
		//ctx.translate(this.x + this.padding + this.size / 2, this.y + this.padding + this.size / 2);
		//ctx.rotate(45 * Math.PI / 180);
		ctx.fillStyle = this.color;
		ctx.fillRect(this.x+this.padding, 
			this.y+this.padding, 
			this.size-this.padding*2, 
			this.size-this.padding*2);

		// Reset transformation matrix to the identity matrix
		//ctx.setTransform(1, 0, 0, 1, 0, 0);

		if (this.isHovered) {
			ctx.strokeStyle = "#00f";
			ctx.strokeRect(this.x, this.y, this.size, this.size);
		}
	}

}

