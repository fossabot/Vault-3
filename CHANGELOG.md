# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.2.4-alpha] - 2021-01-08

### Added
- Implemented UI validation base
- Added profile management related views
- Added license view
- Added CHANGELOG

### Changed
- Updated MVVM framework
  * BindableBase now implements INotifyDataErrorInfo to handle UI Validations. 
  * Models can override the Validate method to implement property validation.
  * ViewModelBase now handles all ICommand registration and notifications.
- Updated setting view
  * Updated to include profile management views
  * Updated to include UI validation rules
- update about view
  * Updated license link to display license view instead of github link
- Defined basic coding guidelines and refactored the codebase to reflect the same.
- Updated auto-versioning to adhere to Semantic Versioning standards defined [here](https://semver.org/spec/v2.0.0.html).
- Bumped version to 0.2

## [0.1.4-alpha] - 2021-01-03

### Added
- Using [MahApps.Metro](https://mahapps.com/) UI framework for consistent UI look and feel.
- Added base MVVM framework.
  * BindableBase class implements the INotifyPropertyChanged to handle model property change notifications. All models, must inherit from this.
  * ViewModelBase implements common ViewModel logic. All ViewModel classes must inherit from this.
- Added main view - Main application window.
- Added about View - About application control, displayed as a fly-out on main window.
- Added settings manager - Singleton class to manage the application settings. Settings are stored in settings.json in the application executable folder.
- Settings View - Setting dialog for the application.
- Application icon from [Google Suits icon set](https://www.iconfinder.com/iconsets/google-suits-1) by [Chamestudio Pvt Ltd](https://www.iconfinder.com/chamedesign)