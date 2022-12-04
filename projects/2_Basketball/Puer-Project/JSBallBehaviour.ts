import { JsBehaviour } from './Base/base'
import { JSGameManager } from './JSGameManager';

class JSBallBehaviour extends JsBehaviour<CS.JsMonoBehaviour>{

    protected prescore: boolean
    OnTriggerEnter(trigger: CS.UnityEngine.Collider) {
        if (trigger == JSGameManager.instance._mb.PrescoreTrigger) {
            this.prescore = true;
        }
        if (trigger == JSGameManager.instance._mb.ScoredTrigger && this.prescore) {
            console.log("得分")
        }
    }
}

export { JSBallBehaviour }