export interface Position {
  x: number;
  y: number;
}

export class Snake {
  body: Position[];
  direction: string;
  constructor() {
    this.body = [{ x: 10, y: 10 }];
    this.direction = "RIGHT";
  }
  setDirection(dir: string) {
    const opposite = {
      UP: "DOWN",
      DOWN: "UP",
      LEFT: "RIGHT",
      RIGHT: "LEFT",
    };
    if (opposite[this.direction as keyof typeof opposite] !== dir) {
      this.direction = dir;
    }
  }
  move() {
    const head = { ...this.body[0]! };
    switch (this.direction) {
      case "UP":
        head.y -= 1;
        break;
      case "DOWN":
        head.y += 1;
        break;
      case "LEFT":
        head.x -= 1;
        break;
      case "RIGHT":
        head.x += 1;
        break;
    }
    this.body.unshift(head);
    this.body.pop();
  }
  grow() {
    const tail = this.body[this.body.length - 1]!;
    this.body.push({ ...tail });
  }
  checkCollision(size: number): boolean {
    const head = this.body[0]!;
    console.log(`Head position: (${head.x}, ${head.y})`);
    if (head.x < 0 || head.y < 0 || head.x >= size || head.y >= size) {
      return true;
    }
    for (let i = 1; i < this.body.length; i++) {
      if (head.x == this.body[i]!.x && head.y == this.body[i]!.y) {
        return true;
      }
    }
    return false;
  }
}
