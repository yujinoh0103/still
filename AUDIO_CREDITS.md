# Audio Credits and Import Guide

이 저장소에는 저작권/라이선스 문제로 실제 오디오 파일을 포함하지 않습니다.
아래 표는 개발자가 로컬에서 음원을 다시 넣을 때 참고하는 링크와 사용 구간입니다.
- 링크가 `TODO`인 항목은 최종 배포 전 출처와 라이선스를 다시 확인해야 합니다.

## 배치 경로

- BGM: `Assets/Sound/Music/`
- SFX: `Assets/Sound/SoundEffect/`

## BGM

| ID | 사용 장면 | 곡/파일명 | 사용 구간 | 권장 파일명 | 출처 링크 | 비고 |
| --- | --- | --- | --- | --- | --- | --- |
| BGM_TITLE | 타이틀 화면 | Dark Memories | 0:00-1:15 | `bgm_title_dark_memories` | TODO | 타이틀 루프/인트로 확인 필요 |
| BGM_AFTER_WORK | 퇴근 장면 | packed | 0:00-0:23 | `bgm_after_work_packed` | TODO | 차가 멈추는 타이밍에 같이 종료 |
| BGM_INTRO | 자기소개 장면 | Breathing | 0:00-0:42 | `bgm_intro_breathing` | TODO |  |
| BGM_EXPLORE | 개인 탐색 | Bird Food | 0:00-0:13 | `bgm_explore_bird_food` | https://artlist.io/royalty-free-music/song/who-are-you-hiding-from/126747 |  |
| BGM_INCIDENT | 범발 이벤트 | atmo.ogg | 0:00-0:18 | `bgm_incident_atmo` | TODO | 현이 긴장한 때부터 놀랄 때까지 |
| BGM_DEBATE | 전체토론 | TrackTribe | 0:00-0:13 | `bgm_debate_tracktribe` | TODO |  |
| BGM_DEATH_SEOYEON | 서연 사망 | zombie main music | 0:00-1:11 | `bgm_death_seoyeon_zombie_main` | TODO |  |
| BGM_DEATH_YERIN | 예린 사망 | ambientmain_0.ogg | 0:00-0:30 | `bgm_death_yerin_ambientmain_0` | TODO |  |
| BGM_DEATH_AIDEN | 에이든 사망 | atmo.ogg | 0:00-0:18 | `bgm_death_aiden_atmo` | TODO |  |
| BGM_END_HAPPY_GOOD | 해피/굿 엔딩 | otts.flac | 전체 | `bgm_end_happy_good_otts` | TODO |  |
| BGM_FLASHBACK | 과거 회상 | e.ogg | 전체 | `bgm_flashback_e` | TODO | 해피/굿 엔딩 내 회상 |
| BGM_END_NORMAL | 노멀 엔딩 | e.ogg | 전체 | `bgm_end_normal_e` | TODO |  |
| BGM_END_BAD | 배드 엔딩 | The Insurgent | 전체 | `bgm_end_bad_the_insurgent` | TODO |  |

## 공통 SFX

| ID | 사용 장면 | 효과음 | 권장 파일명 | 출처 링크 | 비고 |
| --- | --- | --- | --- | --- | --- |
| SFX_ITEM_GET | 아이템 획득 | item pop-up | `sfx_item_get` | https://artlist.io/sfx/track/sci-craft-game---item-pop-up/99680 |  |
| SFX_BUTTON_CLICK | 버튼 클릭 | mouse single click | `sfx_button_click` | https://artlist.io/sfx/track/working-from-home---computer-mouse-single-click-/73415 |  |
| SFX_DIALOG_NEXT | 대사 넘김 | mouse click | `sfx_dialog_next` | https://artlist.io/sfx/track/youtubers-kit-vol-1---best-mouse-click-ever/96334 |  |
| SFX_SLIDING_DOOR | 미닫이문 | wood door open/shut | `sfx_sliding_door` | https://artlist.io/sfx/track/door-wood-antique-room-open-shut/23188 |  |
| SFX_DOOR | 여닫이문 | TODO | `sfx_door` | TODO | 후보 필요 |
| SFX_FOOTSTEP_HYUN | 단체 이동/현 | sneakers on wood | `sfx_footstep_hyun` | https://artlist.io/sfx/track/sneakers-middle-sized-steps-on-wood-floor/17823 |  |
| SFX_FOOTSTEP_SEOYEON | 단체 이동/서연 | sneakers on hard wood | `sfx_footstep_seoyeon` | https://artlist.io/sfx/track/sneakers-middle-sized-steps-on-hard-wood/17811 |  |
| SFX_FOOTSTEP_YERIN | 단체 이동/예린 | shoes on hard wood floor | `sfx_footstep_yerin` | https://artlist.io/sfx/track/shoes-middle-sized-steps-on-hard-wood-floor/17775 |  |
| SFX_FOOTSTEP_AIDEN | 단체 이동/에이든 | barefoot on wood floor | `sfx_footstep_aiden` | https://artlist.io/sfx/track/barefoot-walking-fast-on-wood-floor/17683 |  |

## 스토리/이벤트 SFX

| Story | 사용 장면 | 효과음 | 권장 파일명 | 출처 링크 | 비고 |
| --- | --- | --- | --- | --- | --- |
| 1 | 차 세우는 소리 | car interior beeps/engine stop | `sfx_car_stop` | https://artlist.io/sfx/track/honda-accord---car-interior-beeps-engine-start-stop-/54422 |  |
| 1 | 머리를 치는 둔탁한 소리 | bat whack wet impact | `sfx_hit_head` | https://artlist.io/sfx/track/close-combat---hitting-baseball-bat-whack-wet-impact/50580 | 강도 조절 필요 |
| 2 | 이동 | footsteps | `sfx_move_common` | TODO | 공통 발소리 중 선택 |
| 3-1F | 두꺼비집 클릭 미니게임 | light switch flick | `sfx_light_switch` | https://artlist.io/sfx/track/switches-and-buttons---light-switch-flicking-off/54924 |  |
| 3-1F | 미니게임 시작 | start cue | `sfx_minigame_start` | TODO | 공통 큐 필요 |
| 3-1F | 미니게임 종료 | clear/fail cue | `sfx_minigame_end` | TODO | 공통 큐 필요 |
| 3-1F | 큰방 문 덜컥거림 | shake handle | `sfx_door_handle_rattle` | https://artlist.io/sfx/track/door-plywood-francis-shake-handle/23119 |  |
| 3-1F incident | 기계 위잉 소리 | sewing machine | `sfx_machine_whirr` | https://artlist.io/sfx/track/sewing-machine---sewing-a-seam/73208 |  |
| 3-2F | 고양이 소리 | cat meow | `sfx_cat_meow` | https://artlist.io/sfx/track/farm-animals---cat-meow-small/52699 |  |
| 3-3F | 실험일지 넘김 | flip page | `sfx_page_flip` | https://artlist.io/sfx/track/flip-page-reading-book-paper-/20714 |  |
| 3-3F | 금고게임 오답 | ATM error alert | `sfx_safe_error` | https://artlist.io/sfx/track/credit-card---atm-error-alert/131184 |  |
| 3-3F incident | 똑똑똑 세 번 | mansion door knock | `sfx_knock_three` | https://artlist.io/sfx/track/the-basement---knocking-on-mansion-door-/109229 |  |
| 3-3F incident | 쾅쾅쾅쾅 네 번 | wooden door knocking | `sfx_knock_four_loud` | https://artlist.io/sfx/track/doors-foley---wooden-door-knocking-interior/83758 |  |
| 3-4F | 점프 후 미니게임 시작 | jump cue | `sfx_jump` | TODO | 후보 필요 |
| 3-4F | 베란다 창문/신문지 | newspaper tear | `sfx_newspaper_tear` | TODO | 후보 필요 |
| 3-4F | 칼 정리 미니게임 | knife drop | `sfx_knife_drop` | TODO | 후보 필요 |
| 4 | 단체 이동 | footsteps | `sfx_group_move` | TODO | 캐릭터별 발소리 사용 가능 |
| 5-4F | 버튼 누름 | button click | `sfx_button_click` | https://artlist.io/sfx/track/working-from-home---computer-mouse-single-click-/73415 | 공통 SFX 재사용 |
| 5-4F | 밧줄 꺾임/조명 켜짐 | rope/light cue | `sfx_rope_light_on` | TODO | 후보 필요 |
| 5-3F | 동물 울음소리 | cat meow | `sfx_cat_meow` | https://artlist.io/sfx/track/farm-animals---cat-meow-small/52699 | 토끼 후보가 있으면 교체 가능 |
| 6-good | 현관문 열림 | metal door opening | `sfx_front_door_open` | https://artlist.io/sfx/track/designed-opening-metal-door-dungeon-02/23067 | 노멀 엔딩도 재사용 |