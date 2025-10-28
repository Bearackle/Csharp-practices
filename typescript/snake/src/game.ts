import { Snake } from "./snake.js";
import { Food } from "./food.js";
import { sleep } from "./utils.js";

export class Game {
  canvas: HTMLCanvasElement;
  ctx: CanvasRenderingContext2D;
  size = 20;
  tileCount = 20;
  snake = new Snake();
  food = new Food(this.tileCount);
  running = true;
  speed = 150;

  constructor(canvasId: string) {
    this.canvas = document.getElementById(canvasId) as HTMLCanvasElement;
    this.ctx = this.canvas.getContext("2d")!;
    document.addEventListener("keydown", (e) => this.handleKey(e));
  }

  handleKey(e: KeyboardEvent) {
    switch (e.key) {
      case "ArrowUp":
        this.snake.setDirection("UP");
        break;
      case "ArrowDown":
        this.snake.setDirection("DOWN");
        break;
      case "ArrowLeft":
        this.snake.setDirection("LEFT");
        break;
      case "ArrowRight":
        this.snake.setDirection("RIGHT");
        break;
    }
  }

  draw() {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    this.ctx.fillStyle = "red";
    this.ctx.fillRect(
      this.food.position.x * this.size,
      this.food.position.y * this.size,
      this.size,
      this.size
    );

    this.ctx.fillStyle = "#4CAF50";
    for (const segment of this.snake.body) {
      this.ctx.fillRect(
        segment.x * this.size,
        segment.y * this.size,
        this.size - 1,
        this.size - 1
      );
    }
  }

  async loop() {
    while (this.running) {
      this.snake.move();

      // Ăn thức ăn
      const head = this.snake.body[0]!;
      if (head.x === this.food.position.x && head.y === this.food.position.y) {
        this.snake.grow();
        if (this.snake.body.length === this.tileCount * this.tileCount) {
          this.running = false;
          alert("You Win!");
          break;
        } else this.food.respawn(this.snake.body);
      }

      // Kiểm tra thua
      if (this.snake.checkCollision(this.tileCount)) {
        this.running = false;
        alert("Game Over!");
        break;
      }

      this.draw();
      await sleep(this.speed);
    }
  }
}
