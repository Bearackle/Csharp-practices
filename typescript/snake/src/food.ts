import { Position } from "./snake.js";

export class Food {
  position: Position;

  constructor(private size: number) {
    this.position = this.randomPosition();
  }

  randomPosition(): Position {
    return {
      x: Math.floor(Math.random() * this.size),
      y: Math.floor(Math.random() * this.size),
    };
  }

  respawn(body: Position[]) {
    this.position = this.randomPosition();
    do {
      this.position = this.randomPosition();
    } while (
      body.some(
        (segment) =>
          segment.x === this.position.x && segment.y === this.position.y
      )
    );
  }
}
