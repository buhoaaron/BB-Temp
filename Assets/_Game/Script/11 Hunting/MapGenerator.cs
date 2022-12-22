using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.Hunting
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> pointContainers;

        private List<Point> points = new List<Point>();

        public void GenerateMap(HuntingAsset asset, List<int> targetList, System.Action<int> onClickCharacter)
        {
            points = new();
            Point[] childPotins;
            for(int i = 0; i < pointContainers.Count; i++)
            {
                childPotins = pointContainers[i].GetComponentsInChildren<Point>();
                for (int j = 0; j < childPotins.Length; j++) points.Add(childPotins[j]);
            }

            if(points.Count < targetList.Count)
            {
                Debug.LogError("Point數量不夠");
                return;
            }

            int randomIndex;
            Point tempPoint;
            for (int i = 0; i < points.Count; i++)
            {
                randomIndex = Random.Range(0, points.Count);

                tempPoint = points[i];
                points[i] = points[randomIndex];
                points[randomIndex] = tempPoint;
            }

            Sprite tempSprite;
            List<Sprite> decorateSprites = new(asset.decorateSprites);
            for (int id = 1; id <= asset.barnabusList.Count; id++)
            {
                if (!targetList.Exists(x => x == id)) decorateSprites.Add(asset.barnabusList[id].CharacterImage);
            }

            for (int i = 0; i < decorateSprites.Count; i++)
            {
                randomIndex = Random.Range(0, decorateSprites.Count);

                tempSprite = decorateSprites[i];
                decorateSprites[i] = decorateSprites[randomIndex];
                decorateSprites[randomIndex] = tempSprite;
            }

            int currentDecorateIndex = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (i < targetList.Count)
                {
                    points[i].SetCharacterID(targetList[i]);
                    points[i].SetImage(asset.barnabusList[targetList[i]].CharacterImage);
                    points[i].SetOnClickAction(onClickCharacter);
                }
                else
                {
                    if (currentDecorateIndex >= decorateSprites.Count) currentDecorateIndex = 0;
                    points[i].SetCharacterID(-1);
                    points[i].SetImage(decorateSprites[currentDecorateIndex++]);
                    points[i].SetOnClickAction(null);
                }
            }
        }


        /*
        [SerializeField]
        private Transform objectContainer;

        private List<Point> points = new List<Point>();

        public void GenerateMap(HuntingAsset asset, int objectAmount, List<int> targetList, System.Action<int> onClickCharacter)
        {
            if(objectContainer.childCount < objectAmount)
            {
                Debug.LogError("Point數量不夠");
                return;
            }

            for (int i = 0; i < objectContainer.childCount; i++)
            {
                //objectContainer.GetChild(i).SetSiblingIndex(Random.Range(0, objectContainer.childCount));
                objectContainer.GetChild(Random.Range(0, objectContainer.childCount)).SetSiblingIndex(i);
            }
            for (int i = objectAmount; i < objectContainer.childCount; i++)
            {
                Destroy(objectContainer.GetChild(i).gameObject);
            }

            Sprite temp;
            int randomIndex;
            List<Sprite> decorateSprites = new(asset.decorateSprites);
            for(int i = 0; i < asset.characterSprites.Count; i++)
            {
                if(!targetList.Exists(x => x == i)) decorateSprites.Add(asset.characterSprites[i]);
            }

            for (int i = 0; i < decorateSprites.Count; i++)
            {
                randomIndex = Random.Range(0, decorateSprites.Count);

                temp = decorateSprites[i];
                decorateSprites[i] = decorateSprites[randomIndex];
                decorateSprites[randomIndex] = temp;
            }

            Point point;
            int currentDecorateIndex = 0;
            for (int i = 0; i < objectAmount; i++)
            {
                point = objectContainer.GetChild(i).GetComponent<Point>();
                points.Add(point);

                if (i < targetList.Count)
                {
                    point.SetCharacterID(targetList[i]);
                    point.SetImage(asset.characterSprites[targetList[i]]);
                    point.SetOnClickAction(onClickCharacter);
                }
                else
                {
                    if (currentDecorateIndex >= decorateSprites.Count)
                        currentDecorateIndex = 0;
                    point.SetCharacterID(-1);
                    point.SetImage(decorateSprites[currentDecorateIndex++]);
                    point.SetOnClickAction(null);
                }
            }
        }
        */
    }
}