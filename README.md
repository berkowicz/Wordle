# Wordle

## Intro

This is our attempt to create an interpetation of Wordle.
The program handles all logic serverside and presents the data to the React-client via our viewmodels to minimize data exposure.

## Setup

- MVC with React
- Authorization with JWT
- Entity Framework Core
- Code First with SQL Server

## The Code

| **Helpers**        | **Breakdown**                                                     |
| ------------------ | ----------------------------------------------------------------- |
| GameHelper.cs      | Game logic                                                        |
| HighscoreHelper.cs | Highscore logic to create arrays of top 10 today and alltime      |
| ProfileHelper.cs   | User statistic logic to create array with average values and win% |

<br>

| **Data Base**   | **Breakdown**                                                          |
| --------------- | ---------------------------------------------------------------------- |
| ApplicationUser | Table contains all user data so we can implement autorization with JWT |
| GameModel       | Table contains all gamedata and a has a FK to ApplicationUser          |
| HighscoreModel  | Table contains all successfully completed games with game statistics   |

<br>

| **ViewModels**     | **Breakdown**                      |
| ------------------ | ---------------------------------- |
| FetchGameViewModel | Data needed load open game         |
| GameViewModel      | Data abaout users guess            |
| HighscoreViewModel | Data to present highscore          |
| NewGameViewModel   | GameId (GuiId)                     |
| ProfileViewModel   | Data to present avg user statistic |

## Contributors

<table>
  <tr>
    <td align="center"><a href="https://github.com/berkowicz"><img src="https://avatars.githubusercontent.com/u/112638774?v=4" width="100px;" alt="Daniel Bekowicz"/><br /><sub><b>Daniel Berkowicz</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/wettergrund"><img src="https://avatars.githubusercontent.com/u/50584818?v=4" width="100px;" alt="Jonas Wettergrund"/><br /><sub><b>Jonas Wettergrund</b></sub></a><br />
  </tr>
</table>
