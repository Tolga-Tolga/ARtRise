"""Start and supervise the long-running pipeline processes."""

import signal
import argparse
import os
import subprocess
import sys
import time
from pathlib import Path


SERVER_DIR = Path(__file__).resolve().parents[1]
WATCHER_SCRIPT = SERVER_DIR / "scripts" / "watcher" / "watcher.py"
TRELLIS_SCRIPT = SERVER_DIR / "scripts" / "workers" / "TRELLIS" / "trellis.py"
CONDA_EXE = os.environ.get("CONDA_EXE", "conda")


def command_label(command: list[str]) -> str:
    """Return a concise, repository-relative label for a child command."""

    # The watcher command starts with an absolute script path, while the
    # Conda command starts with the literal subcommand ``run`` and contains
    # its script path later in the argument list.  Only call relative_to on
    # absolute paths; applying it to ``run`` caused startup to abort before
    # the second worker could be launched.
    for argument in command[1:]:
        candidate = Path(argument)
        if not candidate.is_absolute():
            continue
        try:
            return str(candidate.relative_to(SERVER_DIR))
        except ValueError:
            return str(candidate)
    return " ".join(command)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--animate3d-script",
        type=Path,
        default=Path(os.environ.get(
            "ANIMATE3D_SCRIPT",
            SERVER_DIR / "scripts" / "workers" / "Animate_3D" / "idle.sh",
        )),
        help="Shell script used for Animate3D jobs.",
    )
    args = parser.parse_args()
    trellis_env = os.environ.get("ARTRISE_TRELLIS_ENV", "art-rise-trellis")

    processes: list[subprocess.Popen] = []
    stopping = False

    def stop_processes(*_args) -> None:
        nonlocal stopping
        if stopping:
            return
        stopping = True

        for process in processes:
            if process.poll() is None:
                process.terminate()

        deadline = time.monotonic() + 10
        for process in processes:
            remaining = max(0, deadline - time.monotonic())
            try:
                process.wait(timeout=remaining)
            except subprocess.TimeoutExpired:
                process.kill()
                process.wait()

    signal.signal(signal.SIGINT, stop_processes)
    if hasattr(signal, "SIGTERM"):
        signal.signal(signal.SIGTERM, stop_processes)

    commands = [
        [sys.executable, str(WATCHER_SCRIPT), "--animate3d-script", str(args.animate3d_script.resolve())],
        [CONDA_EXE, "run", "--no-capture-output", "-n", trellis_env, "python", str(TRELLIS_SCRIPT)],
    ]

    try:
        for command in commands:
            process = subprocess.Popen(command, cwd=SERVER_DIR)
            processes.append(process)
            print(f"Started: {command_label(command)}", flush=True)

        while not stopping:
            for process in processes:
                return_code = process.poll()
                if return_code is not None:
                    print(f"Process stopped with exit code {return_code}.", flush=True)
                    stop_processes()
                    return return_code
            time.sleep(0.5)
    finally:
        stop_processes()

    return 0


if __name__ == "__main__":
    raise SystemExit(main())
