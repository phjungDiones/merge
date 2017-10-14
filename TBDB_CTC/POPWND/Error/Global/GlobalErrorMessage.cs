using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.POPWND.Error.Global
{
    public class GlobalErrorMessage
    {
        private static volatile GlobalErrorMessage instance;
        private static object syncRoot = new Object();
        public Dictionary<int, Cause_action> _CyborgRobot_HTR = new Dictionary<int, Cause_action>();


        public Dictionary<int, Cause_action> _Nano300 = new Dictionary<int, Cause_action>();

        public Dictionary<int, Cause_action> _AlignerPA300C = new Dictionary<int, Cause_action>();

        public Dictionary<int, Cause_action> _CyMechsRobot = new Dictionary<int, Cause_action>();

        private GlobalErrorMessage()
        {
//            Communication Protocol Errors
//              로봇 운영프로그램에서 Host와 Robot Controller 사이의 통신에서 프로토콜에 정의되지 않은 Message를 수신하였
//              을 때 발생하는 Error 입니다.
//              해당 에러들의 주요 발생 원인은 아래와 같습니다.
//            -  로봇 운영프로그램에서 정의한 Protocol에 알맞지 않은 Command, Argument, 형식이 감지된 경우
//            -  Host에서는 올바른 형식으로 전송하였으나 중간 통신과정에서의 노이즈 등의 문제로 메시지가 손상되어 인식하지
//              못하는 경우
//              해당 에러에서 공통적으로 적용될 수 있는 주요 조치방법은 아래와 같습니다.
//            -  매뉴얼에서 Command 항목을 확인하여, 올바른 Command 및 Argument Value를 입력합니다.
//             - 마지막으로 보낸 커맨드의 Controller측 수신결과는 Teaching Pendent를 통해서 확인 가능합니다.
//              Host에서의 송신 메시지와 일치 여부를 확인해 볼 필요가 있습니다.
//              만약 올바른 Command를 보냈음에도 Controller 측 수신결과가 다른 경우에는 통신과정에서의 문제를 의심해
//              볼 수 있습니다.통신연결상태 체크가 필요합니다.


           _CyborgRobot_HTR.Add(1, new Cause_action("Illegal command", " 로봇 운영프로그램에 정의되지 않은 Command가 수신되는 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(2, new Cause_action("Wrong number of stage", "입력된 Stage Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(3, new Cause_action("Wrong number of arm", "입력된 Arm Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(4, new Cause_action("*Wrong number of slot", "입력된 Slot Argument값이 올바르지 않은 경우 발생합니다.","올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(5, new Cause_action("Illegal speed range", "입력된 Speed Argument값이 올바르지 않은 경우 발생합니다","올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(6, new Cause_action("Wrong number of robot axis", "입력된 Axis Argument값이 올바르지 않은 경우 발생합니다", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(7, new Cause_action("Invalid value of axis location", "입력된 절대/상대위치 Argument값이 올바르지 않은 경우 발생합니다.","올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(8, new Cause_action("*Illegal argument Count*", "입력된 Argument의 개수가 해당 명령어에 올바르지 않은 경우 발생합니다", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(10, new Cause_action("*Invalid argument type* ", "입력된 Argument의 Type이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(12, new Cause_action("*Invalid value of pitch*", "입력된 Pitch Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(13, new Cause_action("*Invalid value of up stroke*", "입력된 Up Stroke Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(14, new Cause_action("*Invalid value of down stroke *", "입력된 Down Stroke Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(15, new Cause_action("*Invalid value of totalnumber of slot *", " 입력된 Max Slot Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(16, new Cause_action("*Invalid value of mapping speed* " , " 입력된 Mapping Speed Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(17, new Cause_action("*Invalid value of referencethickness!!", "입력된 Material Thick Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(18, new Cause_action("*Invalid value of thickness margin!!", "입력된 두께 오차허용비율 Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(19, new Cause_action("*Invalid value of existence margin!!", "입력된 위치 오차허용비율 Argument값이 올바르지 않은 경우 발생합니 다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(27, new Cause_action("*Invalid value of On/Off*", "입력된 On/Off Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(28, new Cause_action("*Invalid signal number*", "입력된 I/O Number Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(29, new Cause_action("*Invalid value of delay time!!", "입력된 WaitTime Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(31, new Cause_action("*Invalid value of retry count on error*", "입력된 RetryCount Argument값이 올바르지 않은 경우 발생합니다", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(33, new Cause_action("*Invalid value of arm distance*", "입력된 Arm 상대/절대 거리 Argument값이 올바르지 않은 경우 발생합 니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(34, new Cause_action("*Invalid value of protruded wafer detect start position*", "입력된 Material 돌출검사 시작거리 Argument값이 올바르지 않은 경 우 발생합니다", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(35, new Cause_action("*Invalid value of protruded wafer detect count!!", "입력된 Material 돌출검사 시작거리 Argument값이 올바르지 않은 경 우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(36, new Cause_action("*Invalid value of clearance* ", " 입력된 Clearance Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(37, new Cause_action("*Invalid value of wafer state*", " 입력된 Wafer State Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(41, new Cause_action("*Invalid value of aligner argument*", "입력된 Aligner 관련 Argument값이 올바르지 않은 경우 발생합니다", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(42, new Cause_action("*Aligner communication time-out error*", "Aligner의 통신 응답이 지정한 시간(60초)을 초과한 경우 발생합니다", "로봇 초기화를 진행한 뒤 다시 해당명령을 수행해 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(51, new Cause_action("*Data Read Busy*", "Flash Memory에 Writing 작업 중 다른 Write가 시도될 경우에 발생합 니다.", "잠시 후 다시 해당 명령을 수행해 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(52, new Cause_action("*Data Write Busy* ", "Flash Memory에 Writing 작업 중 다른 Write가 시도될 경우에 발생합 니다.", " 잠시 후 다시 해당 명령을 수행해 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(53, new Cause_action("*Robot Control Busy*", "Flash Memory에 Writing 작업 중 다른 Write가 시도될 경우에 발생합 니다.", "잠시 후 다시 해당 명령을 수행해 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(54, new Cause_action("*Aligner Busy*", "Align Command 실행 중 다른 Align Command가 시도", "올바른커맨드 및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(61, new Cause_action("*Flash Busy, Try later*", "Flash Memory에 Writing 작업 중 다른 Write가 시도될 경우에 발생합 니다.", "올바른커맨드 및 통신을 확인합니다."));


            //                       Program Initialize Fail Errors
            //           로봇 운영프로그램 실행 시 초기단계에서 각종 설정 값을 Load하는 도중 발생할 수 있는 Error입니다.
            //           해당 에러들의 주요 발생 원인은 아래와 같습니다.
            //           제어기 내부의 로봇 설정파일을 임의로 수정하면서 잘못된 값을 넣었을 경우
            //           제어기 내부 설정파일이 모종의 이유로 삭제된 경우
            //           제어기 Flash 메모리 손상으로 해당 설정파일을 읽지 못하는 경우
            //           해당 에러에서 공통적으로 적용될 수 있는 주요 조치방법은 아래와 같습니다.
            //           Controller의 Error Log를 확인하여 어떠한 내용 혹은 File이 잘못되었는지 확인합니다.
            //           설정파일의 잘못된 내용이 있다면 올바르게 수정
            //           삭제된 파일이 있다면 기존의 백업파일을 이용하여 복구
            //           제어기 Flash 메모리가 손상된 경우라면 기존 데이터 백업 후 제어기 Flash Memory Format 수행


            

            _CyborgRobot_HTR.Add(180, new Cause_action("*File Read Error*", "로봇 운영프로그램에서 파일을 읽는 중 오류발생시 발생합니다.", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(181, new Cause_action("*Pattern file load error*", "로봇 운영프로그램에서 Motion Command 설정 파일을 읽는중 오류발 생시 발생합니다.", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(182, new Cause_action("*Profile file load error*", "로봇 운영프로그램에서 Profile Speed 설정 파일을 읽는 중 오류발생시 발생합니다.", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(184, new Cause_action("*Stage info file load error*", "로봇 운영프로그램에서 티칭 위치정보 파일을 읽는 중 오류발생시 발생 합니다.", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(185, new Cause_action("*Pattern file load error*", "로봇 운영프로그램에서 로봇설정 파일을 읽는 중 오류발생시 발생합니 다", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(186, new Cause_action("*Option file load error*", "로봇 운영프로그램에서 옵션 설정파일을 읽는 중 오류발생시 발생합니", "log확인, 어떤 파일이 손상됬는지 확인후 파일교체 및 수정"));
            _CyborgRobot_HTR.Add(201, new Cause_action("*Robot is busy*", "Motion 명령 실행 중에 Motion 명령 실행 요청이 온 경우 발생합니다.", "현재 수행중인 Motion 명령이 완료된 후 해당 명령을 실행합니다."));
            _CyborgRobot_HTR.Add(202, new Cause_action("*Servo power is off*", "명령실행 시점에 Servo Off 상태인 경우 발생합니다", "INITIAL /R_RESET/SERVOON 명령을 이용해 Servo On 이후 해당 명 령을 실행합니다"));
            _CyborgRobot_HTR.Add(203, new Cause_action("*On E-Stop*", "명령실행 시점에 E-Stop 상태인 경우 발생합니다.", " E-Stop상태를 해지하고 INITIAL /R_RESET/SERVOON 명령을 이용해 Servo On 이후 해당 명령을 실행합니다."));
            _CyborgRobot_HTR.Add(204, new Cause_action("*Robot is paused*", "명령실행 시점에 로봇이 일시 정지 상태인 경우 발생합니다", " R_RSTOP 명령으로 이전 명령을 취소시킨 뒤 해당 명령을 수행합니다."));
            _CyborgRobot_HTR.Add(205, new Cause_action("*Robot is not paused* ", " R_RESUM 명령 수행시점에 Pause 상태가 아닌 경우 발생합니다.", "R_PAUSE 명령으로 Pause 상태인 경우에만 R_RESUM 명령을 실행시 켜 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(206, new Cause_action("*Robot is not executing command*", "로봇이 구동 중이 아닐 때 R_PAUSE/R_RSTOP/SWESTOP 명령을 받 으면 발생합니다", "해당 명령들은 로봇이 구동중인 경우에만 실행시켜 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(207, new Cause_action("*Robot is stopped*", "외부 I/O로 로봇이 Stop 상태인 경우에 명령을 받을 경우 발생합니다", "외부 I/O의 Stop 상태를 해지시킨 뒤 명령을 실행시켜 주시기 바랍니 다."));
            _CyborgRobot_HTR.Add(208, new Cause_action("*Robot has an error*", "Error Reset이 필요한 Error가 발생한 상태에서 Error Reset이 되지 않 았을 때 명령을 받은 경우 발생합니다.", "R_RESET 명령으로 Error를 Reset한뒤 해당 명령을 실행합니다."));
            _CyborgRobot_HTR.Add(211, new Cause_action("*Robot paused by I/O signal*", "R_PAUSE 대신 외부 I/O로 Pause된 상태에서 R_RESUM 명령을 받을 경우 발생합니다.", "외부 I/O의 Pause 상태를 해지하여 실행중인 명령을 완료해 주시기 바 랍니다"));
            _CyborgRobot_HTR.Add(212, new Cause_action(" *Robot is using MCP Mode*", "MCP 사용 중 명령을 받았을 때 발생합니다.", " MCP의 사용이 종료된 이후에 해당 명령을 실행시켜 주시기 바랍니다."));
            _CyborgRobot_HTR.Add(215, new Cause_action("*Robot Stopped by Ext-I/O*", "해당 Stage에 설정된 Gate IO가 꺼져 있는 상태에서 ARM을 뻗을 때 발생합니다.", "해당 Stage에 설정된 Gate IO를 키고 ARM을 뻗습니다."));
            _CyborgRobot_HTR.Add(222, new Cause_action("*Error on wafer status during wafer mapping*", "Material Scan 후 비정상 데이터를 감지하였을 때 발생합니다.", "명령다시실행, 지속적으로 반복 될 경우 Sensor 설정을 확인합니다."));
            _CyborgRobot_HTR.Add(224, new Cause_action("*Error on wafer status before wafer mapping*","Material Scan을 시작하는 시점에 Mapping Sensor에 신호가 감지되었 을 때 발생합니다.", "주변 환경에 센서를 감지 시킬만한 물체에 대해 확인합니다."));
            _CyborgRobot_HTR.Add(232, new Cause_action("*Robot have no map scan data*", "특정 Stage에 Mapping Data를 요청하였지만 해당 Stage가 아직 Scan이 수행되지 않은 경우", " 해당 Stage에 Scan을 먼저 수행한 뒤 Mapping Data를 요청합니다."));
            _CyborgRobot_HTR.Add(291, new Cause_action("*Need Stage Teaching*", "Teaching이 이루어지지 않은 상태에서 해당 Stage로 이동명령을 받았 을 때 발생합니다", "해당 Stage의 Teaching을 수행하고 해당 명령을 실행시킵니다"));
            _CyborgRobot_HTR.Add(292, new Cause_action("*Need Stage Parameter Config*", "Parameter 설정이 이루어지지 않은 상태에서 해당 Stage에 관련된 명 령을 받았을 때 발생합니다.", "해당 Stage의 Parameter 설정을 수행하고 해당 명령을 실행시킵니다."));
            _CyborgRobot_HTR.Add(293, new Cause_action("*This Stage is Flipped Location*", "Flip된 상태로 Teaching된 위치에 Flip 명령을 사용하지 않았을 때 발생 합니다.", " /F 가 붙은 Flip모션 전용 명령어를 사용해 주시기 바랍니다"));
            _CyborgRobot_HTR.Add(294, new Cause_action("*This Stage is not Flipped Location*", "Flip되지 않은 상태로 Teaching된 위치에 Flip 모션명령을 사용하였을 때 발생합니다", "/F 가 붙지 않은 일반 모션 명령어를 사용해 주시기 바랍니다"));
            _CyborgRobot_HTR.Add(295, new Cause_action("*Clearance value is set to be wrong*", "Scara 로봇 구동중 특정 Stage의 Clearance 설정값이 적절하지 않을 때 발생합니다.", "Clearance 값을 재설정합니다."));
           



            //                       Material Handling Errors
            //          Material Handling중 발생할 수 있는 Error 입니다.
            //          발생원인과 조치사항은 Error별로 차이가 있습니다.
            //          Material Handling Error 발생시에는 R_RESET 혹은 INITIAL 명령으로 Error Reset이 수행되어야 이후 다른
            //          Motion 명령의 실행이 가능합니다





            _CyborgRobot_HTR.Add(301, new Cause_action("*Requested arm is handling wafer*", "Material을 가져오는 명령을 수신하였지만(GETFROM등) 이미 해당 Arm에 Material이 존재하는 경우 발생합니다.", " Arm에 Material이 존재하지 않는 상태에서 명령이 수행되어야 합니다."));
            _CyborgRobot_HTR.Add(302, new Cause_action("*Requested arm is not handling wafer*", "Material을 투입하라는 명령을 수신하였지만(PUTINTO등) 해당 Arm에 Material이 존재하지 않는 경우 발생합니다.", "Arm에 Material이 존재하는 상태에서 명령이 수행되어야 합니다."));
            _CyborgRobot_HTR.Add(303, new Cause_action("*Requested arm is not handling wafer*", "Material을 가져오는 명령을 실행하였지만(GETFROM등) Material을 가 져오는데 실패한 경우 발생합니다.", "지정한 위치에 Material의 유무를 확인합니다"));
            _CyborgRobot_HTR.Add(304, new Cause_action("*Requested arm is handling wafer*", ") Material을 투입하라는 명령을 실행하였지만(PUTINTO등) 해당 위치에 투입을 실패한 경우 발생합니다.", "투입을 시도한 Stage를 확인합니다."));
            _CyborgRobot_HTR.Add(312, new Cause_action("*Current robot position is dangerous*", "로봇의 다음 이동이 위험한 Motion을 수행할 것으로 판단되는 경우에 발생합니다", " T/P의 수동조작을 이용하여 로봇을 안전한 위치로 이동시킨 뒤 INITIAL명령을 수행합니다."));







            //            Aligner Errors(CPS-PA20300C - 001)
            //얼라이너 매뉴얼 참조
            //로봇 제어기는 Aligner Error Code에 600을 더한 Error Code를 송신합니다.

            _CyborgRobot_HTR.Add(701, new Cause_action("*Aligner: Busy*", "Aligner가 명령을 수행하는 상태에서 다른 명령을 받으면 발생합니다", " 작업 완료 메시지를 받은 후에 재시도 합니다."));
            _CyborgRobot_HTR.Add(702, new Cause_action("*Aligner: Not Homed*", "Aligner의 최초 Power On 시 Home를 잡지 않고 다른 명령을 받으면 발생합니다.", "ALIGNHOME 명령 후 재시도 합니다.."));
            _CyborgRobot_HTR.Add(703, new Cause_action("*Aligner: Start fail Error*", "Aligner를 초기화를 하지 않은 상태에서 명령을 받게 되면 발생합니다.", "ALIGNHOME 명령 후 재시도 합니다."));
            _CyborgRobot_HTR.Add(709, new Cause_action("*Aligner: Time out*", "정해진 시간(10초) 내에 Align을 완료하지 못했을 때 발생합니다", "웨이퍼의 상태를 확인한 후 재시도 합니다."));
            _CyborgRobot_HTR.Add(711, new Cause_action("*Aligner: Invalid command*", "상위로부터 지정되지 않은 명령을 받았을 경우 발생합니다.", "입력한 명령을 확인 후 재시도 합니다."));
            _CyborgRobot_HTR.Add(712, new Cause_action("*Aligner: Invalid angle*", "상위로부터 유효하지 않은 범위의 각도 값의 명령이 들어왔을 경우 발생 합니다.", "명령의 각도 값의 범위를 확인한 후 재시도 합니다."));
            _CyborgRobot_HTR.Add(721, new Cause_action("*Aligner: No Wafer Error*", "웨이퍼가 없는 상태에서 Align 명령을 받았을 때 발생합니다.", "웨이퍼의 상태를 체크한 후 재시도 또는 공압 시스템을 체크 후 재시도 합니다."));
            _CyborgRobot_HTR.Add(749, new Cause_action("*Aligner: Unexpected Error*", "예상하지 못한 예러가 발생 하였을 경우 발생합니다.", "해당 로봇 업체로 문의 바랍니다"));

            //            Monitoring Errors
            //로봇 운영프로그램은 주기적으로 로봇의 여러 가지 상태를 검사합니다. 본 항목의 Error는 그러한 검사결과 이상이 감
            //지되었을 때 발생하는 Error입니다.


            _CyborgRobot_HTR.Add(801, new Cause_action("*Monitor detected wafer status error*", "Arm이 Material을 들고 있는 상태에서 충돌이나 기구적 문제, 혹은 인 위적으로 Material이 제거될 경우 발생합니다.", "INITIAL 명령이나 R_RESET 명령으로 Error를 Reset합니다."));
            _CyborgRobot_HTR.Add(821, new Cause_action("*Robot Detect Collision*", "로봇 구동 중 충돌이 감지되었을 때 발생합니다. 충돌 감지 시 로봇은 즉시 정지하고 수행중인 명령을 종료합니다.", "INITIAL 명령이나 R_RESET 명령으로 Error를 Reset합니다."));
            _CyborgRobot_HTR.Add(831, new Cause_action("*Ext-I/O State Changed*", "ARM이 뻗어 있는 상태에서 Gate IO가 꺼지면 발생 , 위급상황이기 때문에 SERVO_OFF됩니다.", " INITIAL 명령이나 R_RESET 명령으로 Error를 Reset합니다."));
            _CyborgRobot_HTR.Add(901, new Cause_action("*Robot cannot execute received command *", "해당 로봇이 지원하지 않는 명령을 수신할 경우 발생합니다.", "해당 로봇이 지원 가능한 명령을 이용합니다."));
            _CyborgRobot_HTR.Add(1001, new Cause_action("*Warning:Need Cable Check*", " 일정횟수 이상의 Encoder Cable 통신 이상이 감지되면 발생합니다.", "해당 경고가 자주 발생하는 경우, Cable 상태 점검이 필요합니다. "));




            //            -200 to - 299 Standard System Errors    -1
            // - 300 to - 499 Hardware Device Related Errors      -3
            //-500 to - 699 Input and Output Errors          -5
            //-700 to - 999 Language Related Errors       -7
            // - 1000 to - 1499 Robot Related Errors -10
            //   - 1500 to - 1599 Configuration Parameter Database, Datalogger, and CPU Monitor Errors -15
            //-1600 to - 1699 Controller Errors -16
            //-1700 to - 1799 Network, Socket, and Communication Errors -17
            // - 3000 to - 3999 Servo Related Errors -30
            //   - 4000 to - 4100 Vision Related Errors -40

            _CyborgRobot_HTR.Add(-1, new Cause_action("Standard System Errors", "PA Controller Error 입니다.", "None"));
            _CyborgRobot_HTR.Add(-3, new Cause_action("Hardware Device Related Errors", "PA Controller Error 입니다.", "None"));
            _CyborgRobot_HTR.Add(-5, new Cause_action("Input and Output Errors ", "PA Controller Error 입니다.", "None"));
            _CyborgRobot_HTR.Add(-7, new Cause_action("Language Related Errors", "PA Controller Error 입니다.", "None"));
            //_CyborgRobot_HTR.Add(-7, new Cause_action("Language Related Errors", "PA Controller Error 입니다.", "None"));



























        }
        public static GlobalErrorMessage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalErrorMessage();
                    }
                }

                return instance;
            }
        }











    }
}
