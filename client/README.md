# ARtRise Client

ARtRise is a Unity-based mixed-reality card application for Meta Quest headsets. It recognizes QR-coded cards through the Quest passthrough camera, places the corresponding card UI in the physical environment, and supports multiple game and visualization modes.

This directory contains the Unity client. In the complete ARtRise repository, the matching backend is available in the sibling `server` directory.

## Requirements

- Unity `6000.0.39f1`
- Unity Hub
- Android Build Support for the selected Unity installation:
  - Android SDK & NDK Tools
  - OpenJDK
- Meta Quest 2, Quest Pro, Quest 3, or Quest 3S
- A USB connection with developer mode and USB debugging enabled for building to a headset
- A server on the same network when using Experimental Mode

The project uses Meta XR SDK 81.0.0, OpenXR, XR Interaction Toolkit components, OpenCV for Unity, ZXing.Net, and glTFast. Unity restores registry and Git-based packages from `Packages/manifest.json` and `Packages/packages-lock.json` when the project is opened.

## Getting started

1. Clone the repository.
2. Open the `client` directory in Unity Hub. It is the Unity project root because it contains `Assets`, `Packages`, and `ProjectSettings`.
3. Select Unity `6000.0.39f1` when opening the project.
4. Wait for package resolution and asset import to finish.
5. Open `Assets/Scene/ModeSelection.unity` to start from the mode-selection lobby.

Do not copy Unity's generated `Library`, `Temp`, `Logs`, or `UserSettings` directories between machines. Unity recreates them locally.

## Available scenes

The scenes included in the build settings are:

- `ModeSelection` — the mode-selection lobby
- `A` — game mode A
- `B` — game mode B
- `C` — game mode C
- `Experimental` — server-backed model loading and replacement

## Experimental Mode

Selecting Experimental from the mode-selection lobby opens a server connection dialog before the scene is loaded.

The default connection is:

```text
IP address: 192.168.178.69
Port:      18082
```

The values can be changed in the dialog. The resulting URL is stored in Unity PlayerPrefs and reused by the Experimental scene.

### Client/server file contract

For a card with ID `1`, the client expects the server to provide one of these static model files:

```text
/files/download/1.glb
/files/download/1_out.glb
```

After the static model is loaded, the client polls for the animated model:

```text
/files/animated/1.gltf
```

When the animated file becomes available, it replaces the static model at runtime.

The client collects six valid artwork screenshots per QR code to preserve the existing recognition and capture logic. Once six screenshots have been collected, only the first screenshot is uploaded, using the client-side filename `<cardId>.png`, for example:

```text
1.png
```

The upload endpoint is:

```text
POST /files/upload
```

The current Unity client sends the card ID as the filename. The matching ARtRise server validates and preserves that basename so the upload and model identifiers remain consistent.

### Network requirements

- The Quest and server must be connected to the same reachable network.
- The configured server port must be open in the server machine's firewall.
- The server must listen on a LAN-reachable address, not only on `localhost`.
- The Unity project allows HTTP because the current development server uses `http://` rather than HTTPS. For production, HTTPS is recommended.

### Manual files on the Quest headset

For the supplied study setup, copy the contents of `client/install_manually` to the Quest 3S after installing the APK.

Open this folder on the headset:

```text
This PC\Quest 3S\Internal shared storage\Android\data\com.UlmUniversity.ARtRise\files
```

Copy the folders inside `client/install_manually` into this `files` directory. Do not copy the `install_manually` folder itself. The resulting structure must be:

```text
files/
├── gameobjects/
│   └── preloaded/
│       ├── 1.glb
│       ├── 2.glb
│       └── ...
└── StudyLogs/
    └── ...study log files...
```

The folder name `StudyLogs` must keep this capitalization because it is the default folder configured in the Unity client. Unlock the headset and select File Transfer when connecting it over USB if the `Android/data` directory is not visible.

## Building for Meta Quest

1. Connect the Quest headset over USB and accept the USB debugging prompt.
2. In Unity, open `File > Build Profiles` and select Android.
3. Confirm that the scenes listed above are included in the build.
4. Build an APK or use `Build And Run`.
5. Install the generated APK on the headset.

The Android application identifier is:

```text
com.UlmUniversity.ARtRise
```

The product name is `ARtRise`.

## Troubleshooting

### The app cannot upload images or download models

Check the following:

- The IP address and port in the Experimental dialog are correct.
- The Quest can reach the server over the local network.
- The server is running and exposes `/files/upload` and `/files/download/...`.
- The configured port is reachable through the server firewall.
- The APK was rebuilt after changing the Unity project settings.

The client defaults to port `18082`, while the Spring Boot server defaults to `8080`. Start the server with `SERVER_PORT=18082` or enter `8080` in the client dialog.

The Unity Player Settings contain `insecureHttpOption: 2`, and the Android manifest enables cleartext traffic for the local HTTP development server.

### The QR scanner does not detect cards

Make sure the headset has passthrough camera permission and that the QR code is sufficiently visible and well lit. The scanner uses ZXing.Net, which is included under `Assets/Packages/ZXing.Net.0.16.10`.

### Models are not visible in Experimental Mode

Inspect the Android logcat output for the following messages:

```text
[FloorCubeConsumerExperimental] Loading model 1
[FloorCubeConsumerExperimental] Downloaded 1.glb
[FloorCubeConsumerExperimental] Downloaded animated 1.gltf
```

The static model is used as the initial replacement. The animated model is installed later when the server makes it available.

## Repository layout

```text
client/
├── Assets/
│   ├── Scene/
│   ├── Samples/3 QRCodeTracking/
│   ├── Plugins/Android/
│   ├── Packages/ZXing.Net.0.16.10/
│   └── ...
├── Packages/
├── ProjectSettings/
├── install_manually/
├── release/
├── .gitignore
└── README.md
```

The `release/` directory is intended for local APK artifacts. APK files are ignored by Git because the current build is larger than GitHub's regular-file limit. Upload the APK as an asset of a GitHub Release instead of committing it to the repository history.

## Privacy and credentials

Do not commit access tokens, Wit.ai credentials, private certificates, personal log files, or other machine-specific secrets. Review any configuration assets before publishing the repository. The default server IP is a private LAN address and can be replaced in the application UI.

## License

The repository is distributed under the license in the root `LICENSE` file. Third-party packages and assets retain their own licenses; verify them before redistributing bundled SDKs or sample content.
